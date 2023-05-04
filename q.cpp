#include <string>
#include <stdint.h>
#include <stdbool.h>
#include <stdio.h>
#include <Arduino.h>

typedef void (*Func)(void *);

template <typename I, typename O>
struct COMPILER_PARAMETERS
{
    TaskHandle_t taskhandle;
    I *input;
    O *output;
};

template <typename I, typename O>
void *COMPILER_INVOKE(I inputParameters, O outputParameters, Func func, int isAsync, int isAwait)
{
    TaskHandle_t taskhandle = xTaskGetCurrentTaskHandle();

    COMPILER_PARAMETERS<void, void> parameters;
    parameters.taskhandle = taskhandle;
    parameters.input = &inputParameters;
    parameters.output = outputParameters;

    if (isAsync)
    {
        xTaskCreate(func, NULL, 4096, &parameters, 10, &taskhandle);
        if (isAwait)
        {
            xTaskNotifyWait(0, 0, NULL, portMAX_DELAY);
        }
    }
    else
    {
        func(&parameters);
    }

    return parameters.output;
}

typedef struct COMPILER_INPUT_STRUCT_initialize_values
{
    int32_t a;
    int32_t b;

} COMPILER_INPUT_STRUCT_initialize_values;
typedef struct COMPILER_OUTPUT_STRUCT_initialize_values
{
    int32_t c;
    int32_t d;

} COMPILER_OUTPUT_STRUCT_initialize_values;
#define COMPILER_PARAMETERS_initialize_values COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_initialize_values, COMPILER_OUTPUT_STRUCT_initialize_values>
typedef struct COMPILER_INPUT_STRUCT_x
{
    int32_t y;

} COMPILER_INPUT_STRUCT_x;
typedef struct COMPILER_OUTPUT_STRUCT_x
{

} COMPILER_OUTPUT_STRUCT_x;
#define COMPILER_PARAMETERS_x COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_x, COMPILER_OUTPUT_STRUCT_x>
typedef struct COMPILER_INPUT_STRUCT_print_abcd
{
    int32_t a;
    int32_t b;
    int32_t c;
    int32_t d;

} COMPILER_INPUT_STRUCT_print_abcd;
typedef struct COMPILER_OUTPUT_STRUCT_print_abcd
{

} COMPILER_OUTPUT_STRUCT_print_abcd;
#define COMPILER_PARAMETERS_print_abcd COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_print_abcd, COMPILER_OUTPUT_STRUCT_print_abcd>
typedef struct COMPILER_INPUT_STRUCT_setup
{

} COMPILER_INPUT_STRUCT_setup;
typedef struct COMPILER_OUTPUT_STRUCT_setup
{

} COMPILER_OUTPUT_STRUCT_setup;
#define COMPILER_PARAMETERS_setup COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_setup, COMPILER_OUTPUT_STRUCT_setup>

void initialize_values(void *pvParameters)
{

    ((COMPILER_PARAMETERS_initialize_values *)pvParameters)->output->c = (((COMPILER_PARAMETERS_initialize_values *)pvParameters)->input->a);

    ((COMPILER_PARAMETERS_initialize_values *)pvParameters)->output->d = (((COMPILER_PARAMETERS_initialize_values *)pvParameters)->input->b);

    return;
};
void x(void *pvParameters)
{

    return;
};
void print_abcd(void *pvParameters)
{
    Serial.println((((COMPILER_PARAMETERS_print_abcd *)pvParameters)->input->a));
    Serial.println((((COMPILER_PARAMETERS_print_abcd *)pvParameters)->input->b));
    Serial.println((((COMPILER_PARAMETERS_print_abcd *)pvParameters)->input->c));
    Serial.println((((COMPILER_PARAMETERS_print_abcd *)pvParameters)->input->d));

    return;
};
void setup()
{
    COMPILER_OUTPUT_STRUCT_print_abcd _30015890;
    Serial.begin(115200);
    COMPILER_OUTPUT_STRUCT_initialize_values _21083178;
    COMPILER_OUTPUT_STRUCT_initialize_values _55530882;
    COMPILER_INVOKE ([]()
                    { return COMPILER_INPUT_STRUCT_print_abcd{COMPILER_INVOKE([]()
                                                                              { return COMPILER_INPUT_STRUCT_initialize_values{1, 2}; }(),
                                                                              &_21083178, initialize_values, 0, 0),
                                                              COMPILER_INVOKE([]()
                                                                              { return COMPILER_INPUT_STRUCT_initialize_values{3, 4}; }(),
                                                                              &_55530882, initialize_values, 0, 0)}; }(),
                    &_30015890, print_abcd, 0, 0);

    return;
};

void loop()
{
    delay(10);
}