#include <Arduino.h>


typedef struct my_struct {
    int value;
    int value2;
} my_struct;


QueueHandle_t queue;
uint32_t notify = 0;
static void CalledFunc(void *pvParameters) {
    static my_struct our_struct = {1, 2};

    queue = xQueueCreate(10, sizeof(my_struct));
    if (queue == 0) {
        printf("Failed to create queue= %p\n", queue);
    }

    while (1) {
        if (xQueueSend(queue, (void *)&our_struct, (TickType_t)5) != pdPASS) {
            printf("Failed to send to the queue.\r");
        }

        vTaskDelay(1000 / portTICK_RATE_MS);
    }
}

static void AwaitedFunc(void *pvParameters) {
    my_struct rxBuffer;
    while (1) {
        if (xQueueReceive(queue, &(rxBuffer), (TickType_t)5) == pdPASS) {
            Serial.printf("Received data from queue %d %d \n", rxBuffer.value, rxBuffer.value2);
            // printf("Received data from queue == %s\n", rxBuffer.value);
        }
    }
}

void setup() {
    Serial.begin(115200);
    while (!Serial) {};

    Serial.println("");
    Serial.println("******************************");
    Serial.println("        Program start         ");
    Serial.println("******************************");

    xTaskCreate(CalledFunc, "Task A", 4096, NULL, 10, NULL);
}

void loop() {
    // NOTHING
}