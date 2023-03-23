#include <Arduino.h>

TaskHandle_t Handle_aTask;
TaskHandle_t Handle_bTask;
QueueHandle_t queue;

typedef struct {
  int arg1;
  int arg2;
} inputTuple;

inputTuple _inputTuple;

typedef struct {
  int output1;
  int output2;
} outputTuple;

void ThreadA(void *pvParameters)
{
  inputTuple *params = (inputTuple *)pvParameters;
  outputTuple output;

  queue = xQueueCreate(5, sizeof(outputTuple));
  
  while (1) {
    Serial.printf("Param 1: %d, param 2: %d\n", params->arg1, params->arg2);

    output.output1 = params->arg1 * 10;
    output.output2 = params->arg2 + 20;

    xQueueSend(queue, &output, (TickType_t)0);

    vTaskDelay(1000 / portTICK_RATE_MS);
  }
}

void ThreadB(void *pvParameters)
{
  outputTuple threada_data;

  while (1)
  {
    if (xQueueReceive(queue, &threada_data, (TickType_t)5) == pdTRUE)
    {
      printf("Data from ThreadA: %d %d\n", threada_data.output1, threada_data.output2);
    }
  }
}

void setup()
{
  Serial.begin(115200);
  while (!Serial);

  Serial.println("");
  Serial.println("******************************");
  Serial.println("        Program start         ");
  Serial.println("******************************");

  _inputTuple.arg1 = 1;
  _inputTuple.arg2 = 2;

  xTaskCreate(ThreadA, "Task A", 4096, &_inputTuple, 10, &Handle_aTask);
  xTaskCreate(ThreadB, "Task B", 4096, NULL, 10, &Handle_bTask);
}

void loop()
{
  // NOTHING
}
