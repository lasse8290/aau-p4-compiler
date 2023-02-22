#include <stdio.h>
#include "./pt/pt.h"
#include <time.h>
#include <dirent.h>


#define SECOND 1000000 
#define TICKS_PER_MILLISECOND 1000

// Declare protothreads
static struct pt pt1, pt2;
clock_t start;
#include<sys/time.h>

int millis () {
  clock_t current = clock();
  return (int)(current - start);
}

// 1st protothread function to blink LED 1 every second
static int printBoogie(struct pt *pt)
{
  static unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  while(1) {    
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 1 * SECOND);
    printf("\nBoogie!\n");
  }
  PT_END(pt);
}

static int checkFilesChanges(struct pt *pt)
{
  unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  while(1) {
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 5 * SECOND);
    printf("\nWonderland\n");
  }
  PT_END(pt);
}

static int slowPrintFile(struct pt *pt, char* filename)
{

  unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  
  FILE *fp;
  char line[100];

   fp = fopen(filename, "r");
   if (fp == NULL) {
      printf("Error opening the file.\n");
      return -1;
   }

   fclose(fp);
  while(1) {
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 500 * TICKS_PER_MILLISECOND);
    printf("maybe?");
    if (fgets(line, 100, fp) != NULL)
      printf("%s\n", line);
  }
  PT_END(pt);
}

int fileCount = -1;

int DirectoryChanged() {
    int localFileCount = 0;
    struct dirent *dir;
    DIR* d = opendir("./files");
    if (d) {
        while ((dir = readdir(d)) != NULL)
        {
            localFileCount++;
        }
        closedir(d);
    }
    if (fileCount != -1) {
      int tempFileCount = fileCount;
      fileCount = localFileCount;
      return tempFileCount != localFileCount;
    }
    
    return 0;
}

// In setup, set all LEDs as OUTPUT, push button as INPUT, and
// init all protothreads
void setup() {
  start = clock();
  PT_INIT(&pt1);
  PT_INIT(&pt2);
}
// In the loop we just need to call the protothreads one by one
void loop() {
    // printBoogie(&pt1);
    // checkFilesChanges(&pt2);
  
    slowPrintFile(&pt1, "./files/testfile.txt");
}

int main() {
    setup();
    while (1) {
      loop();
    }
}