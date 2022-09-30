# AvaloniaUWP

Experimental UWP implementation for Avalonia. 


## About

I forked this [https://github.com/pingzing/avalonia-uwp-fork](avalonia-uwp-fork) project.

This is draft only. UWP Library seems to be not completed. No samples... so.. sorry. As is.

## 2 super-crazy ideas from [m][e] 

- "Avalonia Engine"-based XBOX UWP apps
- "Avalonia Engine"-based ARM64 UWP apps, hehe (not for W10M... but... who knows if you get early builds of Avaloniaui 0.5...)

## Words of the author

"I'm looking to build a cross-platform GUI host and had the crazy idea of using assemblies of Avalonia XAML and codebehind be the meat-and-potatoes of the framework, but started thinking about how to get such a framework into the Microsoft Store. Could UWP host an app that renders Avalonia XAML? Then I found this. I haven't had time to work with it yet, but I'm curious if you have any documentation on what led you to do this and any follow up plans or articles?"
- Neil McAlister


## Current status / Plan for future experiments

Experimenting with a theoretical UWP target for Avalonia, and have some thoughts: 

UWP can support .NET Standard from 1.4 to 2.0. Is it worth restricting core components to 1.4 for some time just for that? 

The iOS and Android backends are hosted on an AppDelegate and a MainActivity, respectively. 

The UWP equivalent to this would be an Application. 

What if I RnD old commints of Avalonia... idk :)


## Referencies

- [Neil McAlister](https://github.com/pingzing)
- [AvaloniaUI](https://github.com/AvaloniaUI/)
- [pingzing's avalonia-uwp-fork](avalonia-uwp-fork) 
- [Gitter Discussion](https://gitter.im/AvaloniaUI/Avalonia?at=59cd68ff614889d4754ff3c7)

## ..


AS IS. No support. RnD only. DIY


## .

- [m][e] 2022, September