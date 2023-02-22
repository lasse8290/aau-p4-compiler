#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <dirent.h>
#include <sys/time.h>
#include "./pt/pt.h"

#define TICKS_SECOND 1000000
#define TICKS_MILLISECOND TICKS_SECOND / 1000

int fileCount = -1;
int directoryChanged();

// Declare protothreads
static struct pt pt1, pt2;
clock_t start;

int millis()
{
  clock_t current = clock();
  return (int)(current - start);
}

// 1st protothread function to blink LED 1 every second
static int printBoogie(struct pt *pt)
{
  static unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  while (1)
  {
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 1 * TICKS_SECOND);
    printf("\nBoogie!\n");
  }
  PT_END(pt);
}

static int checkFilesChanges(struct pt *pt)
{
  unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  while (1)
  {
    PT_WAIT_UNTIL(pt, directoryChanged());
    printf("Directory has changed!");
    printf("File count: %d\n", fileCount);
  }
  PT_END(pt);
}

int directoryChanged()
{
  int localFileCount = 0;
  struct dirent *dir;
  DIR *d = opendir("./files");
  if (d)
  {
    while ((dir = readdir(d)) != NULL)
    {
      if (dir->d_type == DT_REG)
        localFileCount++;
    }
    closedir(d);
  }

  if (fileCount != -1)
  {
    int tempFileCount = fileCount;
    fileCount = localFileCount;
    return tempFileCount != localFileCount;
  }

  fileCount = localFileCount;
  return 0;
}

// In setup, set all LEDs as OUTPUT, push button as INPUT, and
// init all protothreads
void setup()
{
  start = clock();
  PT_INIT(&pt1);
  PT_INIT(&pt2);
}
// In the loop we just need to call the protothreads one by one
void loop()
{
  printBoogie(&pt1);
  checkFilesChanges(&pt2);
}

int main()
{
  setup();
  while (1)
  {
    loop();
  }
}