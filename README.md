# Oiski's Park'n Wash

## About The Project
This repository is part of the `Oiski.School` namespace collection, which includes projects build as school assignments.

### Terms of Development
`Park 'N Wash` needs us to build a system that can keep track of parking arrangements and is open for further extensions if necessary.
The program must make use of tickets to keep track of said arrangements by having a client _Check in ->
Get Ticket. Check Out; Bill Ticket_ and display the information back to the client.
The price of the _Bill_ is based on _how long_ the clients occupates a parking Spot and _which type_ of parking spot
the occupation involves.

`Park'N Wash` plans on extending their services to include 3 car wash facilities, which must be kept in mind under development.

- **Specifications**
  - **Parking**
    - 50 Standard Parking Spots
    - 10 Utility Parking Spots
    - 12 Large Parking Spots
    - 5 Handicap Parking Spots
    - Only one client can occupy a spot at a time.
  - **Car Wash**
    - Specs to come...
- **Ticket System**
  - **Parking**
    - A Basic Ticket
    - A Ticket for parking spots that include charge stations
    - A Ticket that includes a car wash
    - A Ticket that includes a service check (_If a clients parking arrangements extends to at least 48 hours_)
  - **Car Wash**
    - Specs to come...
- **Data Storage**
  - The initial storage arrangement is agreed to be `.csv` files.
    - **Files**
      - `ParkingSpots.csv`
      - `Tickets.csv`
  - The storage arrangement may change, so in that regard the program must be open for such an event.
    in the we store data.
- **Interface**
  - No specific requirements about layout
  - Must be able to show a client the amount of available parking spots within each type of spot.

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
- **[v0.0.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.0.0)**
  - Setup of the base structure for the relationship between modules
- **[v0.0.0-Rework](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.0.0-Rework)**
  - Reworked the core structure for the relationship between core modules and how they will interact with eachother. \
    The new structure allows for completely generic service system modules, which can be injected into the main flow of the program without rebuilding the core structure.
- **[v0.1.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.1.0)**
  - **Implemented `ServiceHandler`**
    - Ability to inject/remove services
    - Ability to convert IMyServiceBase to any convertable type
- **[v0.2.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.2.0)**
  - **Implemented `ParkingService`**
    - Ability to Store Parking Spots
    - Ability to Add/Remove Parking Spots from collection
    - Ability to Occupie Parking Spot
    - Ability to Cancel Occupation of Parking Spot
    - Ability to find Parking Spot based on Predicate
    - Ability to find a collection of Parking Spots based on Predicate
    - Ability to Validate Parking Spot
    - Ability to Change Service ID
  - **Changes to ParkAndWash**
    - Ability to convert generic types
- **[v0.3.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.3.0)**
  - **Implemented `TicketService`**
    - Ability to store Tickets
    - Ability to Ad/Remove Tickets from collection
    - Ability to generate Tickets for occupations
    - Ability to Cancel/Destroy Tickets of occupations
    - Ability to Find Ticket based on Predicate
    - ability to find a collection of Tickets based on Predicate
    - Ability to Validate Tickets
    - Ability to change Service ID
- **[v0.4.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.4.0)**
  - **Implemented `Factory`**
    - Ability to create Parking Services
    - Ability to create Ticket Services
    - Ability to create Parking Spots
    - Ability to create Tickets
- **[v0.5.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.5.0)**
  - **Implemented `ParkingRepository`**
    - Ability to insert Parking Spots in storage
    - Ability to update Parking Spots in storage
    - Ability to delete Parking Spots in storage
    - Ability to collect an enumerable object of Parking Spots in storage
  - **Changes to `Factory`**
    - Ability to create default `ParkingSpot` instance
- **[v0.6.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.6.0)**
  - **Implemented `TicketRepository`**
    - Ability to insert Tickets in storage
    - Ability to update Tickets in storage
    - Ability to delete Tickets in storage
    - Ability to collect an enumerable object of Tickets in storage
  - **Changes to `Factory`**
    - Ability to create default `Ticket` instance
- **[v0.6.1](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.6.1)**
  - Added summaries
- **[v0.7.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.7.0)**
  - This version contains _errors_ that are logged and will be _fixed_ along with the extension of the repository system in _v0.8.0_
  - **Extended Ticket System**
    - Reworked the ticketing system to be more scalable
    - Added `IMyParkingTicket` interface
    - Replaced `Ticket` class with `ParkingTicket` class to seperate parking tickets from future car wash tickets
    - Added `ParkingChargeTicket` class
    - Added `ParkingWashTicket` class
    - Added `ParkingServiceTicket` class
- **[v0.8.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.8.0)**
  - **Extended Repository System**
    - Introduced `IMyRepositoryEntity` interface
    - `ParkingSpot` class now implements `IMyRepositoryEntity`
    - `ParkingTicket` now implements `IMyRepositoryEntity`
    - Changed `IMyRepository` interface to be more generic
    - Restructured `ParkingRepository` to follow the new structure of `IMyRepository`
    - Restructured `TicketRepository` to follow the new structure of `IMyRepository`
- **[v0.8.1 - Revision 1](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.8.1)**
  - **Changes to `Factory`**
    - Ability to create all types of Parking Tickets
  - **Changes to Ticket System**
    - Fixed `ParkingTicket` class not Implementing the extended variant of the `IMyTicket` hierarchy
    - Ability to set properties through base interface
    - Ability to collect properties through base interface
    - Introduced PropertyNotFoundException`, which is thrown when a property that does not exist or unavailable is requested
- **[v0.8.2 - Revision 2](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.8.2)**
  - Added proper `Exception` information to method summaries
- **[v0.9.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.9.0)**
  - **Added `Oiski.Common` Library**
    - Added a Converter Class that can cast genric types
    - Added a FileHandler Class that can manipulate text files
- **[v0.9.1 - Revision 3](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.9.1)**
  - **Changes to `ParkingRepository` and `TicketRepository`**
    - Implemented `FileHandler`
  - **Changes to `ParkingSpot` and `ParkingTicket`**
    - ID is now saves as "ID[id]" in file. _Example: "ID1"_
  - **Changes to `ParkAndWash`**
    - Removed `ConvertGeneric` method, as it's no longer needed. The `Oiski.Common.Generic.CastGeneric` serves as a replacement
- **[v0.10.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.10.0)**
  - **Implemented `CarWashService`**
    - Ability to store Car Washes
    - Ability to Add/Remove Car Washes
    - Ability to request a car wash
    - Ability to cancel Car Washes that are running
    - Ability to find Car Washes based on Predicate
    - Ability to find a collection of Car Washes based on predicate
    - Ability to Validate Car Washes
    - Ability to change Service ID
    - Introduced `IMyCarWash` Interface
    - Introduced `CarWash` abstract class
    - Introduced `BronzeWash` class
    - Introduced `SilverWash` class
    - Introduced `GoldWash` class
    - Introduced `CarWashType` enum
    - Introduced `CarWashState` Enum
  - **Fixed**
    - Corrected a spelling mistake in `IMyServiceCollection`
- **[v0.10.1 - Factory Extension](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.10.1)**
  - **Changes to `Factory`**
    - Ability to create Car Washes
    - Ability to create Car Wash Service
    - Prepared for extension of Tickets to include `IMyCarWashTicket` objects
- **[v0.10.2 - Revision 4](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.10.2)**
  - **Changes to the Car Wash System**
    - Deleted BronzeWash class
    - Deleted SilverWash class
    - Deleted GoldWash class
  - **Changes to `CarWash`**
    - Generalized `CarWash` class and removed abstract modifier
    - Introduced `CarWashRunningException`
    - Car Wash rutines are now detached from class objects and moved to instance objects
  - **Changes to `CarWashService`**
    - Add Service Item method now throws the proper exception
  - **Changes to `Factory`**
    - Converted old class object based `CarWash` object creation to the new system, which is based on instance objects
  - **Changes to `ParkingWashTicket`**
    - Changed Wash Type property to make use of `CarWashType` enum instead of being a `string`


## Oiski.School Namespace Collection
1. [Oiski.School.Library_h1_2020](https://github.com/ZhakalenDk/Oiski.School.Library_H1_2020)
2. [Oiski.School.Bank_H1_2020](https://github.com/ZhakalenDk/Oiski.School.Bank_H1_2020)
3. [Oiski.School.RainStatistic_H2_2021](https://github.com/ZhakalenDk/Oiski.School.RainStatistic_H2_2021)
4. [Oiski.School.FitnessLevel_H2_2021](https://github.com/ZhakalenDk/Oiski.School.FitnessLevel_H2_2021)
