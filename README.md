Alferd-Spritesheet-Unpacker
===========================

## What is this?
ASU is a simple spriting tool which takes an animation spritesheet image and automatically turns each sprite into an individual image.
#### The interesting bit
So the actual logic to find the individual sprites can be found in `ASU\BO\RegionUnpacker.vb`.
#### To use
Just double click the .sln file.

## Projects
#### /ASU
This is the main Windows app and contains all the forms.
#### /ForkandBeard.Util
Contains some misc classes I've used in other projects. ASU uses to find rectangle bounds and distances.
#### /ForkandBeard.Logic
Again contains some misc classes I've used in other projects. ASU uses it for Exception handling and getting version numbers.
#### /ImageQuantizers
ASU uses this one to preserve a bitmap's palette index - which the Mugen community go crazy over if you destroy a bitmap's indexed palette. I didn't wrote this project (forget where I found it).

## Code quality
When I wrote this app I was working for a company which worked in VB and one of their dev standards was Hungarian notation, so it was just easiest for me, at that time, to write to that standard for ASU.
I also, originally, never planned on releasing it, so it was only ever going to be a tool for me, which will hopefully explain some of its shoddyness.
Ideally I'd like to move all the code over to C# and remove all the Hungarian notation and other kak.
###### Updates
06/11/2014 -  All VB code has now been replaced with C#
