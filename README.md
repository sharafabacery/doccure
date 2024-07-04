# Doccure Medical System

An Medical platform for clinics that serve and connects doctors and patients.
App has 3 primary users (patient,doctor,admin)
first 2 can create account and connect to each other.
for patient it could be like a portable electronic health record (EHR).

## Features (Complete)

[✔] Admin Dashboard for Users

[✔] Search for doctor

[✔] Save Medical Records

[✔] Manage Appoitments (Doctor specify days and patient register)

[✔] Review Doctor Appointment

[✔] Add Symptoms or Diagnoses.

[✔] Add Doctor's profile and clinic information

[✔] Forget Password Feature

[✔] Login with 3rd party Authuntication

## Features (Not-Complete) Still Under-Development

[X] Text Chating

[X] Video Chating

[X] Seeding Database



## Installation 

Install Doccure-medical-system required packages

Run the command to run migration.

```bash 
  update-database
```

Add Admin User manully to SQLSERVER And  Its Rules

Add secerts of google clientid

```bash 
  dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
  dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
```

## Tech Stack

**Client:** HTML , CSS , JS , Jquery

**Server:** asp.net Core , SQLSERVER

## Authors

- [@Sharaf Abacery](https://github.com/sharafabacery)


## Related

This App was templete based for Frontend ,I download the Templete Thin I Create Backend.
This App still in Development Mode Could be Some Bugs I need T fix.

## Feedback

If you have any feedback, please reach out to us at abacerysharaf@gmail.com

  
