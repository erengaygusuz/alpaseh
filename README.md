# Alpaseh (Hesapla)

## Description

When I was a child, I usually used calculator for my education. Sometimes, I thought that these numbers looks like letters in my mind. 

I turned the calculator 180 degrees clockwise and realise that some word can be derived with using these numbers. 

We can derive the word goggles using these numbers.

* G : 9 or 6
* O : 0
* G : 9 or 6
* G : 9 or 6
* L : 7
* E : 3
* S : 5

You can see that when we turned these number 180 degree in clockwise, we obtain the letters as below.

![Alt text](/images/calculator-with-letters.png)

As a result, I tried to find all words which includes these letters in English and Turkish Language and put them inside the game.

## Tools That I Used During Development

* VContainer Dependency Injection Tool (For Dependency Injection)
* Visual Studio Community Edition 2019 (as a code editor)
* Gimp & Power Point (To create some ui images)
* Vectezy and Flaticon (To add some ui icons and images)
* Audacity (To edit sound files)

## Project Usage

* Be sure that Unity 2020.3.37f1 version is installed in your PC.
* Clone the project using this command: ``` https://github.com/erengaygusuz/alpaseh.git ```
* Run the project with Unity.

## Project Architecture and Folder Structure

In this project, I decided to use a dependency injection tool named VContainer to seperate concerns, to make project more understandable, readable, expandable.

The project architecture at below:

![Alt text](/images/project-architecture.png)

The project assemblies at below:

* FTRGames.Alpaseh.Enums.asmdef
* FTRGames.Alpaseh.LifeTimeScopes.asmdef
* FTRGames.Alpaseh.Models.asmdef
* FTRGames.Alpaseh.Models.LocalizationData.asmdef
* FTRGames.Alpaseh.Presenters.asmdef
* FTRGames.Alpaseh.Services.asmdef
* FTRGames.Alpaseh.Views.asmdef

The project folder structure at below:

* Audios
* Fonts
* Images
* Plugins
* Prefabs
* Resources
* Scenes
* Scripts
  * Enums
  * LifeTimeScopes
  * Models
  * Presenters
  * Services
  * Views

## Game Rules

In this game, there are five levels. In every level, there is a word list. All word list count can be differ from each other. For example: first list has five words, second list twelve words etc.

Word letter count also differ from each other. It begins with three and ends with seven. For example: see (3) and goggles (7).

First level word list start with three letters words and other comes increasingly.

In every level, there are some situations can be change when level increase. These are earning score, loosing time, loosing life, earning life, earning time etc.

You can see the matching list of numbers and letters is at below.

* 0 : O, o
* 1 : I, ı
* 2 : Z, z
* 3 : E, e
* 4 : H, h
* 5 : S, s
* 6 : G, g
* 7 : L, l
* 8 : B, b
* 9 : G, g 

## Related Links

-- 

## License

The MIT License (MIT)

## Screenshots

![Alt text](/images/screenshots/1.jpg)
![Alt text](/images/screenshots/2.jpg)
![Alt text](/images/screenshots/3.jpg)
![Alt text](/images/screenshots/4.jpg)
![Alt text](/images/screenshots/5.jpg)
![Alt text](/images/screenshots/6.jpg)
![Alt text](/images/screenshots/8.jpg)
![Alt text](/images/screenshots/10.jpg)