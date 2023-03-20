#include <Arduino.h>

TaskHandle_t Handle_aTask;
TaskHandle_t Handle_bTask;

typedef struct
{
    int param1;
    float param2;
} TaskParams;

TaskParams params;

static void ThreadA(void *pvParameters)
{
    TaskParams *params = (TaskParams *)pvParameters;

    Serial.println("Thread A: Started");

    while (1)
    {
        int param1 = params->param1;
        float param2 = params->param2;

        Serial.println("Hello World!");

        Serial.print("Param 1: ");
        Serial.println(param1);
        Serial.print("Param 2: ");
        Serial.println(param2);

        delay(1000);
    }
}

static void ThreadB(void *pvParameters)
{
    Serial.println("Thread B: Started");

    for (int i = 0; i < 10; i++)
    {
        Serial.println("---This is Thread B---");
        delay(2000);
    }
    Serial.println("Thread B: Deleting");
    vTaskDelete(NULL);
}

void setup()
{
    Serial.begin(115200);
    while (!Serial)
        ; // Wait for Serial terminal to open port before starting program

    Serial.println("");
    Serial.println("******************************");
    Serial.println("        Program start         ");
    Serial.println("******************************");

    /*parameter params = {1, 2};

    Serial.print((long)&params, DEC);*/

    params = *(TaskParams *)pvPortMalloc(sizeof(TaskParams));
    params.param1 = 42;
    params.param2 = 3.14;

    xTaskCreate(ThreadA, "Task A", 1000, &params, tskIDLE_PRIORITY + 2, &Handle_aTask);
    xTaskCreate(ThreadB, "Task B", 1000, &params, tskIDLE_PRIORITY + 1, &Handle_bTask);
}

void loop()
{
    // NOTHING
}