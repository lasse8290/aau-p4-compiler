#include <Arduino.h>
#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <dirent.h>
#include <sys/time.h>
#include <pt.h>

#define LED_1_PIN 26
#define LED_2_PIN 27
#define BUTTON_PIN 16

int blinkLight()
{
  static int light_state = 0;
  static int lastTimeBlink = 0;
  while (1)
  {
    switch (light_state)
    {
    case 0: // Wait time passed
      if (millis() - lastTimeBlink > 1000)
      {
        light_state = 1;
        lastTimeBlink = millis();
        digitalWrite(LED_2_PIN, HIGH);
      }
      return 1;

    case 1: // Wait blink start
      if (millis() - lastTimeBlink > 100)
      {
        light_state = 0;
        digitalWrite(LED_2_PIN, LOW);
      }
      return 1;
    }
  }
}

int pushButton()
{
  static int button_state = 0;
  while (1)
  {
    switch (button_state)
    {
    case 0: // Wait button pressed
      if (digitalRead(BUTTON_PIN) == HIGH)
      {
        button_state = 1;
        digitalWrite(LED_1_PIN, HIGH);
      }
      return 1;

    case 1: // Wait button released
      if (digitalRead(BUTTON_PIN) == LOW)
      {
        button_state = 0;
        digitalWrite(LED_1_PIN, LOW);
      }
      return 1;
    }
  }
}

void setup()
{
  pinMode(LED_1_PIN, OUTPUT);
  pinMode(LED_2_PIN, OUTPUT);
  pinMode(BUTTON_PIN, INPUT);
}

void loop()
{
  blinkLight();
  pushButton();
}

/*
#include <Arduino.h>
#include <stdio.h>
#include <time.h>
#include <unistd.h>
#include <dirent.h>
#include <sys/time.h>
#include <pt.h>

#define LED_1_PIN 25
#define LED_2_PIN 26
#define LED_3_PIN 27
#define BUTTON_PIN 16

// Declare 3 protothreads
static struct pt pt1, pt2, pt3;
// First protothread function to blink LED 1 every 1 second
static int protothreadBlinkLED1(struct pt *pt)
{
  static unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  while (1)
  {
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 1000);
    digitalWrite(LED_1_PIN, HIGH);
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 1000);
    digitalWrite(LED_1_PIN, LOW);
  }
  PT_END(pt);
}
// Second protothread function to blink LED 2 every 0.5 second
static int protothreadBlinkLED2(struct pt *pt)
{
  static unsigned long lastTimeBlink = 0;
  PT_BEGIN(pt);
  while (1)
  {
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 500);
    digitalWrite(LED_2_PIN, HIGH);
    lastTimeBlink = millis();
    PT_WAIT_UNTIL(pt, millis() - lastTimeBlink > 500);
    digitalWrite(LED_2_PIN, LOW);
  }
  PT_END(pt);
}
// Third protothread function to power on LED 3 if
// the push button is pressed.
static int protothreadPushButton(struct pt *pt)
{
  static unsigned long lastTimeCheck = 0;
  PT_BEGIN(pt);
  while (1)
  {
    lastTimeCheck = millis();
    PT_WAIT_UNTIL(pt, digitalRead(BUTTON_PIN) == HIGH);
    digitalWrite(LED_3_PIN, HIGH);
    PT_WAIT_UNTIL(pt, digitalRead(BUTTON_PIN) == LOW);
    digitalWrite(LED_3_PIN, LOW);
  }
  PT_END(pt);
}
// In setup, set all LEDs as OUTPUT, push button as INPUT, and
// init all protothreads
void setup()
{
  pinMode(LED_1_PIN, OUTPUT);
  pinMode(LED_2_PIN, OUTPUT);
  pinMode(LED_3_PIN, OUTPUT);
  pinMode(BUTTON_PIN, INPUT);
  PT_INIT(&pt1);
  PT_INIT(&pt2);
  PT_INIT(&pt3);
}
// In the loop we just need to call the protothreads one by one
void loop()
{
  protothreadBlinkLED1(&pt1);
  protothreadBlinkLED2(&pt2);
  protothreadPushButton(&pt3);
}
*/