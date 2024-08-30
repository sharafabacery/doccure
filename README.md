# Doccure Medical System

An Medical platform for clinics that serve and connects doctors and patients for reservation and Add medicine for patient and add price of all what doctor do with patient.
App has 3 primary users (patient,doctor,admin)
first 2 can create account and connect to each other.
for patient it could be like a portable electronic health record (EHR) for future.

## Features (Complete)

[✔] Admin Dashboard for Users

[✔] Search for doctor

[✔] Save Medical Records

[✔] Manage Appoitments (Doctor specify days and patient register)

[✔] Review Doctor Appointment

[✔] Add Symptoms or Diagnoses.

[✔] Add Doctor's profile and clinic information

[✔] Forget Password Feature

[✔] Login with 3rd party Authuntication (Google)

[✔] Text Chating

[✔] Video Chating

[✔] Seeding Database

## Installation 

1) Install Doccure-medical-system required packages

2) Go To and genrate appsettings.json 
  2.1) Configure Database Connection and Create new database with name you choose in db connection
  2.2) Configure MailProvider Section For Mail Service
  2.3) Configure UseSeeder bool to activate seeder of database
   
3) Run the command to run migration.

```bash 
  update-database
```

4) secerts of google clientid

```bash 
  dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
  dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
```

## Tech Stack

**Client:** HTML , CSS , JS , Jquery

**Server:** .net core, asp.net Core , SignalR , WebRTC , SQLSERVER

## Authors

- [@Sharaf Abacery](https://github.com/sharafabacery)


## Related

This App was templete based for Frontend ,I download the Templete Thin I Create Backend.
This App still in Development Mode Could be Some Bugs I need T fix.

## Feedback

If you have any feedback, please reach out to us at abacerysharaf@gmail.com

  
