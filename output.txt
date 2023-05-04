#include <string>
#include <stdint.h>
#include <stdbool.h>
#include <Arduino.h>


typedef void (*Func)(void*);
template <typename I, typename O>
struct COMPILER_PARAMETERS {
  TaskHandle_t taskhandle; 
  I input;
  O *output;
};

template <typename I, typename O>
void* COMPILER_INVOKE(I inputParameters, O *outputParameters, Func func, int isAsync, int isAwait) {
    TaskHandle_t taskhandle = xTaskGetCurrentTaskHandle();

    auto parameters = new COMPILER_PARAMETERS<I, O> {
      taskhandle,
      inputParameters,
      outputParameters,
    };

    if (isAsync) {
      xTaskCreate(func, NULL, 4096, parameters, 10, &taskhandle);
      if (isAwait) {
        xTaskNotifyWait(0, 0, NULL, portMAX_DELAY);
      }
    } else {
      func(parameters);
    }

    return outputParameters;
}

typedef struct COMPILER_INPUT_STRUCT_setup {
  
} COMPILER_INPUT_STRUCT_setup;
typedef struct COMPILER_OUTPUT_STRUCT_setup {
  
} COMPILER_OUTPUT_STRUCT_setup;
#define COMPILER_PARAMETERS_setup COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_setup, COMPILER_OUTPUT_STRUCT_setup>
typedef struct COMPILER_INPUT_STRUCT_between {
  
} COMPILER_INPUT_STRUCT_between;
typedef struct COMPILER_OUTPUT_STRUCT_between {
  
} COMPILER_OUTPUT_STRUCT_between;
#define COMPILER_PARAMETERS_between COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_between, COMPILER_OUTPUT_STRUCT_between>


void setup () {
    COMPILER_OUTPUT_STRUCT_between _55530882;
 Serial.begin(115200);
int32_t b;
int32_t x;
Serial.println((String)"before");
COMPILER_INVOKE([&](){
    return COMPILER_INPUT_STRUCT_between {  };
}(), &_55530882, between, 1, 1);
Serial.println((String)"after");

return;

};
void between (void *pvParameters) {
     Serial.println((String)"between");
vTaskDelay(500);
xTaskNotify(((COMPILER_PARAMETERS_between*) pvParameters)->taskhandle, 0, eNoAction);
vTaskDelete(NULL);
delete pvParameters;

return;

};


void loop() {
  delay(10);
}