# Oiski's Park'n Wash

## About The Project
This repository is part of the `Oiski.School` namespace collection, which includes projects build as school assignments. \


## Dependencies
Requires [Oiski.ConsoleTech (v1.5.1)](https://github.com/ZhakalenDk/Oiski.ConsoleTech.Engine/releases/download/v1.5.1/OiskiEngineV1.5.1-Hotfix.zip) to run!

### How To
Unzip the file and place Oiski.ConsoleTech.Engine.dlll and Oiski.ConsoleTech.Engine.xml inside _Oiski.School.ParkAndWash_H2_2021\Oiski.School.ParkAndWash_H2_2021\Oiski.School.ParkAndWash_H2_2021.Application_


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
    - A Basic Car Wash Ticket
    - 3 Car Wash Facilities
    - Car Washes must be able to run simultaneously
- **Data Storage**
  - The initial storage arrangement is agreed to be `.csv` files.
    - **Files**
      - `ParkingSpots.csv`
      - `Tickets.csv`
      - `CarWashes.csv`
  - The storage arrangement may change, so in that regard the program must be open for such an event.
    in the we store data.
- **Interface**
  - No specific requirements about layout
  - Must be able to show a client the amount of available parking spots within each type of spot.
  - Must be able to show a client statistics about Car Washes

## The program
The assignments states that the following criteria:

**Goal**
>Implement OOP Principles (_Solid_) in a user-relevant context, as well as extend and demonstrate ones capabilities within the relevant topics of the course.
>

**Input**
> The application features a navigational UI system that let's the user interact with the different parts system.

**Output**
> The program generates no direct output other than the UI elements displayed in the console window. However, it generates 3 `.csv` files as described above.

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
- **[v0.11.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.11.0)**
  - **Changes to Ticket System**
    - Introduced `CarWashTicket` class
    - Introduced `Ticket` class
    - Introduced `IMyPropertyAccessor` interface
    - Added Type Property to Tickets
  - **Changes to `TicketRepository`**
    - Extended `TicketRepository` to include all current and future Ticket types
  - **Changes to `Factory`**
    - Ability to create default Tickets of any type 
    - Ability to create Car Wash Tickets
- **[v0.11.1 - Revision 5](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.11.1)**
  - **Changes to `Carwash`**
    - Added the ability to check how far a wash has come, in procentage
- **[v0.11.2 - Revision 6](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.11.2)**
  - **Changes to `CarWash`**
    - Ability to keep track of how many times a car wash has been started
    - Ability to save/ load a Car Wash through the Repository system
- **[v0.12.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.12.0)**
  - **Changes to Repositories**
    - Added `CarWashRepository` class
  - **Changes to `CarWash`**
    - Fixed a state save error
- **[v0.13.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.13.0)**
  - **Parking Inteface**
    - Added a Main Screen with 3 options
      - _Parking Service_
      - _Car Wash Service_
      - _Pay Ticket_
    - Added a Parking Screen where requests for parking spots can be performed
    - Added a Ticket Screen for displaying tickets
    - Added a class diagram over the new interface system
    - Added [OiskiEngine.dll](https://github.com/ZhakalenDk/Oiski.ConsoleTech.Engine) to make use of its advanced rendering functionality
  - **Changes to `ParkingRepository`**
    - Fixed `NullReferenceException`
  - **Changes to `Parking Service`**
    - Fixeed `ServiceDuplicateException`
  - **Changes to `ParkingSpot`**
    - Fixed missing ID counter
  - **Changes to `IMyTicket`**
    - Fixed bad description
  - **Changes to `CarWash`**
    - Fixed tick count not resetting between wash sessions
  - **Changes to `CarWashRunningException`**
    - Fixed misleading exception message
- **[v0.14.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.14.0)**
  - **Car Wash Interface**
    - Added `CarWashScreen`class
    - Changed constructor for `BaseScreen` to protected
    - Changed constructors of all derived `BaseScreen` instances to private
    - Moved CombiSelect Method into `BaseScreen` class
    - Added Statistics Button to `MainScreen` class
    - Added setup of car wash service to application
    - Added `CarWashScreen` class to diagram
  - **Changes to `CarWashRepository`**
    - Fixed UpdateDate always returning null
  - **Changes to `CarWashService`**
    - Fixed RequestServiceItem always returning null
  - **Changes to Data Storage System**
    - Fixed wrong data entry in `ParkinSpot`
    - Fixed wrong data entry in `ParkinChargeTicket`
    - Fixed wrong data entry in `ParkingTicket`
  - **Changes to `IMyPropertyAccessor`**
    - Fixed wrong method name
  - **Changes to `TicketService`**
    - Fixed wrong property ID when creating default was ticket
  - **Changes to `TicketScreen`**
    - Fixed spot fee label and value not being hidden
- **[v0.15.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.15.0)**
  - **Implemented `StatisticsScreen`**
    - Ability to display amount of runs for each car wash
    - Ability to display progress for each car wash
    - Ability to display state for each car wash
    - Ability to cancel each car wash
    - Added `StatisticsScreen` class to diagram
  - **Changes to `CarWash`**
    - Fixed an issue where the car wash coulnd't be started after it was previously completed
    - Fixed an issue where an aborted car wash would continue to be aborted, even after starting a new wash.
  - **Changes to `CarWashService`**
    - Fixed an issue where a CarWash would get the wrong wash rutine
  - **Changes to `IMyCarWash`**
    - Renamed CancelWash to AbortWash
- **[v0.16.0](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v0.16.0)**
  - **Implemented `PaymentScreen`**
    - Added `PaymentScreen` class
    - Display finalized data for a ticket
    - Added `PaymentScreen` to diagram
  - **Changes to `ParkingService`**
    - Added data storing to AddServiceItem
    - Added data storing to CancelServiceItem
    - Added data storing to RemoveServiceItem
  - **Changes to `Program`**
    - Removed Repository calls for parking spots and car washes
  - **Changes to `TicketService`**
    - Added data storing to AddServiceItem
    - Added data storing to CancelServiceItem
    - Added data storing to RemoveServiceItem
  - **Changes to `CarWashService`**
    - Added data storing to AddServiceItem
    - Added data storing to RemoveServiceItem
  - **Changes to `TicketScreen`**
    - Ability to remove and delete tickets when they've been payed.
    - Fixed total price value not displaying as DKK
    - Back button text will now show "Pay" instead of "Back" when screen is in a finalizing state
- **[v1.0.0 - Release](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v1.0.0)**
  - Revision for Release State
  - Changed `ServiceDuplicateException` to be public instead of internal
  - **Changes to `ParkingServiceTicket`**
    - Fixed wrong property identifier
  - **Changes to `TicketService`**
    - Fixed a crash due to cast mismatch when a parking ticket was canceled
    - Added occupation logic when requesting a parking ticket
  - **Changes to `ParkingScreen`**
    + Fixed a crash when a ticket was attempted to be created when a ticket from storage with the ame ID already existed.
  - **Changes to `PaymentScreen`**
    - Adjusted postition of ID field
  - **Changes to `TicketScreen`**
    - Fixed an issue where the back button for the `TicketScreen` didn't reset to "Back" after a payment.
    - Fixed an issue where the total cost wasn't calculated as it should.
    - Added Service modifier detection (_Applied = Time > 48_)
  - **Changes to `Program`**
    - Ability to load tickets from storage
- **[v1.0.1 - Hotfix 1](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v1.0.1)**
  - Fixed buffer issue
  - Fixed a `CarWash` attempting to crerate a car wash with an ID that already exists
- **[v1.0.2 - Hotfix 2](https://github.com/ZhakalenDk/Oiski.School.ParkAndWash_H2_2021/releases/tag/v1.0.2)**
  - Fixed parking spots not building with the right decimal value
  - Fixed car washes missing a wash ID after being build from storage
  - Fixed `TicketRepository.UpdateData` always returning false
  - Fixed buffer mismatch
  - Fixed not being able to start a car wash after a tiocket was generated for it

## [Oiski.School Namespace Collection](https://github.com/Mike-Mortensen-Portfolio) <-- Click Me
1. [Oiski.School.Library_h1_2020](https://github.com/ZhakalenDk/Oiski.School.Library_H1_2020)
2. [Oiski.School.Bank_H1_2020](https://github.com/ZhakalenDk/Oiski.School.Bank_H1_2020)
3. [Oiski.School.RainStatistic_H2_2021](https://github.com/ZhakalenDk/Oiski.School.RainStatistic_H2_2021)
4. [Oiski.School.FitnessLevel_H2_2021](https://github.com/ZhakalenDk/Oiski.School.FitnessLevel_H2_2021)
