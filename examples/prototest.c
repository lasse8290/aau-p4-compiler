#include <unistd.h>
#include <sys/time.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include "./pt/pt.h"
#include <time.h>


struct pt pt;

struct timer { int start, interval; };
struct timer timer;

int fiveSecondFunc() {
    for (int i = 0; i < 100; i++) {
        printf("100s function: elapsed seconds %d\n", i);
        sleep(1);
    }
    printf("5s function: completed\n");
}

int read_data() {

}

PT_THREAD(example(struct pt *pt))
{
  PT_BEGIN(pt);
 
  while(1) {
    if(initiate_io()) {
      timer_start(&timer);
      PT_WAIT_UNTIL(pt,
         io_completed() ||
         timer_expired(&timer));
      read_data();
    }
  }
  
  PT_END(pt);
}

int main() {
    fiveSecondFunc();
    return EXIT_SUCCESS;
}

