#include <Servo.h>  // Include the Servo library to control the servo motor

// Define the pins for the ultrasonic sensor
const int TRIG_PIN = 9;  
const int ECHO_PIN = 10; 

// Create a servo object
Servo servoNumber1;

// Declare variables to store the sensor's pulse duration and the calculated distance
float duration, distance;

void setup() {
  // Set the trigger pin as output and the echo pin as input
  pinMode(TRIG_PIN, OUTPUT);
  pinMode(ECHO_PIN, INPUT);

  // Start the serial communication at 9600 baud rate for debugging
  Serial.begin(9600);

  // Attach the servo to pin 2 and set its initial position to 0 degrees
  servoNumber1.attach(2);
  servoNumber1.write(0);
}

void loop() {
  // Set the servo to 0 degrees at the beginning of the loop (reset position)
  servoNumber1.write(0);

  // Start the ultrasonic sensor by setting the trigger pin low, then high, then low again to create a pulse
  digitalWrite(TRIG_PIN, LOW); 
  delayMicroseconds(2);         
  digitalWrite(TRIG_PIN, HIGH); 
  delayMicroseconds(10);        
  digitalWrite(TRIG_PIN, LOW);  

  // Measure the time it takes for the pulse to return (duration in microseconds)
  duration = pulseIn(ECHO_PIN, HIGH);

  // Calculate the distance based on the duration of the pulse
  distance = (duration * 0.0343) / 2;

  // Print the distance to the Serial Monitor for debugging purposes
  Serial.print("Distance: ");
  Serial.println(distance);

  // Short delay before the next reading
  delay(100);

  // Check if the measured distance is less than 50 cm
  if (distance < 50) {
    servoNumber1.write(0);
    delay(500); // Hold at 0 degrees for 500 milliseconds

    // Move the servo to 90 degrees after a short delay
    servoNumber1.write(90);
    delay(500); 
  }
}
