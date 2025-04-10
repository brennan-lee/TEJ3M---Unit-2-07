/*
 * Created by: Brennan Lee
 * Created on: Apr 2025
 * This program gets distance using an ultrasonic sensor
 * and controls a servo based on that distance.
 */

#include <Servo.h>  // Include the Servo library to control the servo motor

// Constants for ultrasonic sensor
const int TRIG_PIN = 9;
const int ECHO_PIN = 10;

// Constants for servo control
const int SERVO_PIN = 2;
const int SERVO_INITIAL_ANGLE = 0;
const int SERVO_TRIGGER_ANGLE = 90;

// Constants for distance calculation
const float SPEED_OF_SOUND_CM_PER_US = 0.0343;  // Speed of sound in cm/µs
const int DISTANCE_THRESHOLD_CM = 50;           // Distance threshold in cm

// Servo object
Servo servoNumber1;

// Variables for  measurement
float duration;
float distance;

void setup() {
  // Set up sensor pins
  pinMode(TRIG_PIN, OUTPUT);
  pinMode(ECHO_PIN, INPUT);

  // Set up serial communication
  Serial.begin(9600);

  // Attach and initialize the servo
  servoNumber1.attach(SERVO_PIN);
  servoNumber1.write(SERVO_INITIAL_ANGLE);
}

void loop() {
  // Reset the servo to initial angle at start of loop
  servoNumber1.write(SERVO_INITIAL_ANGLE);

  // Trigger the pulse
  digitalWrite(TRIG_PIN, LOW);
  delayMicroseconds(2);
  digitalWrite(TRIG_PIN, HIGH);
  delayMicroseconds(10);
  digitalWrite(TRIG_PIN, LOW);

  // Read pulse duration
  duration = pulseIn(ECHO_PIN, HIGH);

  // Calculate distance (cm)
  distance = (duration * SPEED_OF_SOUND_CM_PER_US) / 2;

  // Debug output to Serial Monitor
  Serial.print("Distance: ");
  Serial.print(distance);
  Serial.println(" cm");

  // Small delay before next reading
  delay(100);

  // If object is closer than the threshold, move servo
  if (distance < DISTANCE_THRESHOLD_CM) {
    servoNumber1.write(SERVO_INITIAL_ANGLE);
    delay(500);
    servoNumber1.write(SERVO_TRIGGER_ANGLE);
    delay(500);
  }
}
