Alferd-Spritesheet-Unpacker
===========================

## What is this?
ASU is a simple spriting tool which takes an animation spritesheet image and automatically turns each sprite into an individual image.

## The interesting bit
So the actual logic to find the individual sprites can be found in `SpriteSheetUnpacker\RegionUnpacker.vb`.

## Code quality
When I wrote this app I was working for a company which worked in VB and one of their dev standards was Hungarian notation, so it was just easiest for me, at that time, to write to that standard for ASU.
I also, originally, never planned on releasing it, so it was only ever going to be a tool for me, which will hopefully explain some of its shoddyness.
Ideally I'd like to move all the code over to C# and remove all the Hungarian notation and other kak.
