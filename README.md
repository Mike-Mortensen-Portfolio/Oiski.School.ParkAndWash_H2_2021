# Oiski's Park'n Wash

## About The Project
This repository is part of the `Oiski.School` namespace collection, which includes projects build as school assignments.

## The program
The assignments states that the following criteria:

**Goal**
>Implement OOP Principles (_Solid_) in a user-relevant context.

**Input**
> Description needed...

**Output**
> Description needed...

See the [Wiki](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/wiki) for more in depth information about the project.

## Versioning
The Assignment specifies that versioning should be done according to the following template: [_Major_].[_Minor_].[_Path_].\
Each `Feature` must be branched out and developed on an isolated branch and merged back into the `Developer` branch when done.

The syntax for the structure of folders must be presented as: [DeveloperName]/[Version]/[BranchName], where as branches should be named as follows: [*Version*]-[*Feature*]_[*SubFeature*].\
**Example:**
>**Folder Structure:** _Oiski/v1_ \
>**Branch Name:** _1.0.0-Interface_MainMenu_ \
>**Full Path:** _Oiski/v1/1.0.0-Interface_MainMenu__

### Change Log
- [v0.0.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.0.0)
  - Setup of the base structure for the relationship between modules
- [v0.0.0-Rework](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.0.0-Rework)
  - Reworked the core structure for the relationship between core modules and how they will interact with eachother. \
    The new structure allows for completely generic service system modules, which can be injected into the main flow of the program without rebuilding the core structure.
- [v0.1.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.1.0)
  - Implemented `ServiceHandler`
    - Ability to inject/remove services
    - Ability to convert IMyServiceBase to any convertable type
- [v0.2.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.2.0)
  - Implemented `ParkingService`
    - Ability to Store Parking Spots
    - Ability to Add/Remove Parking Spots from collection
    - Ability to Occupie Parking Spot
    - Ability to Cancel Occupation of Parking Spot
    - Ability to find Parking Spot based on Predicate
    - Ability to find a collection of Parking Spots based on Predicate
    - Ability to Validate Parking Spot
    - Ability to Change Service ID
  - Changes to ParkAndWash
    - Ability to convert generic types
- [v0.3.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.3.0)
  - Implemented `TicketService`
    - Ability to store Tickets
    - Ability to Ad/Remove Tickets from collection
    - Ability to generate Tickets for occupations
    - Ability to Cancel/Destroy Tickets of occupations
    - Ability to Find Ticket based on Predicate
    - ability to find a collection of Tickets based on Predicate
    - Ability to Validate Tickets
    - Ability to change Service ID
- [v0.4.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.4.0)
  - Implemented `Factory`
    - Ability to create Parking Services
    - Ability to create Ticket Services
    - Ability to create Parking Spots
    - Ability to create Tickets
- [v0.5.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.5.0)
  - Implemented `ParkingRepository`
    - Ability to insert Parking Spots in storage
    - Ability to update Parking Spots in storage
    - Ability to delete Parking Spots in storage
    - Ability to collect an enumerable object of Parking Spots in storage
  - Changes to Factory
    - Ability to create default `ParkingSpot` instance
- [v0.6.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.6.0)
  - Implemented `TicketRepository`
    - Ability to insert Tickets in storage
    - Ability to update Tickets in storage
    - Ability to delete Tickets in storage
    - Ability to collect an enumerable object of Tickets in storage
  - Changes to Factory
    - Ability to create default `Ticket` instance

## Oiski.School Namespace Collection
1. [Oiski.School.Library_h1_2020](https://github.com/ZhakalenDk/Oiski.School.Library_H1_2020)
2. [Oiski.School.Bank_H1_2020](https://github.com/ZhakalenDk/Oiski.School.Bank_H1_2020)
3. [Oiski.School.RainStatistic_H2_2021](https://github.com/ZhakalenDk/Oiski.School.RainStatistic_H2_2021)
4. [Oiski.School.FitnessLevel_H2_2021](https://github.com/ZhakalenDk/Oiski.School.FitnessLevel_H2_2021)
