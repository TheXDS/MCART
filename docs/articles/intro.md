# About MCART

## (Pre-)history
A long time ago, when I started learning programming *circa* 1999~2000, I had the experience of starting out in programming languages like GWBASIC and QuickBASIC. These are languages that did not offer many tools or built-in functionality by default, which was relatively common for legacy languages of the time.

Over time, I began to develop small programs or small games with quite limited interactivity, all in QuickBASIC. Because QB was such a limited environment, I had to create various functions that performed small useful tasks in my programs. From simple functions for centering text on the screen to somewhat complex functions that drew text boxes for data input, complete with support for tabbing, scrolling, and password characters.

Originally called XDS!SUBS, that project became my repository of general-purpose utility functions, and although it could not see the public light, I considered that project a great personal achievement. All this in 2004, at the age of 11.

I made some programs using functionality from XDS!SUBS. One of the most impressive was Basic Shell 3, which was simply a graphical layer between MS-DOS and the user. It simply added mouse support and some buttons with ready-to-execute MS-DOS commands, in addition to running in graphical mode (screen mode 12) and offering some special commands for color changes or to change the drawing mode of BS3.

Eventually, I learned Visual Basic. At that time, I started with VBA in Microsoft Office 2003 and then moved on to VB6. I tried to port my functions from XDS!SUBS to Visual Basic, and obviously, the vast majority of them could not work, as they were graphical functions dealing with text mode or other direct drawing modes for MS-DOS environments.

Some of my experiments in Visual Basic benefited from my functions, such as small calculators and tools that dealt with binary numbers. I even tried to invent a password storage format called DUF, which was ridiculously insecure.


## Starting to Take Shape
It is important to highlight that, at this point, XDS!SUBS stopped having that name. I used the files from what was XDS!SUBS as a place for experimentation, where I tried crazy ideas or implemented self-imposed challenges to ensure I understood certain concepts well. I was migrating my skills from VB6 to the relatively new VB.Net (the 2002 version, in 2006).

The project still did not have a name as such. However, being a fan of motorsport, I had the idea to name this great place for all kinds of functions after my favorite racetrack: Nürburgring.

Nürburgring did not have a particular goal. It was simply a place to store code and try interesting ideas. It included a test app where the use of the built-in functions was simply demonstrated, allowing a new developer to take them as examples and use them within their programs.

Among the functions available in Nürburgring was an interpreter for the esoteric programming language [BrainFuck](https://en.wikipedia.org/wiki/Brainfuck), an adapter class for performing AES encryption, checksum functions, binary to numeric type parsers, a class for reading/writing the previously mentioned DUF file format (database of users file), an extremely rudimentary database engine based on XML files, among other functionalities, some of which were incomplete/broken or existed in the .Net Framework.

After several revisions and versions, removing strange and unusual experiments and all the functionality for which there was a native alternative in the BCL of .Net Framework, what would eventually become MCART began to take shape, around the year 2010 while I was finishing school.

Today, I have a repository for strange and unusual experiments, [Vulcanium](https://github.com/TheXDS/Vulcanium/).

## Focusing the Project
The last version packed with dubious utility functionality was [0.3.3.80](https://github.com/TheXDS/MCART-Classic). At this point, I finally decided what my project should be. I ultimately decided that I could create a general-purpose library that could truly provide developers with something useful. Version 0.4 would finally carry the name MCART (Morgan's CLR Advanced Runtime), although it was still referred to by the name Nürburgring.

MCART 0.4 was completely rewritten, removing a large amount of functionality that was not useful or for which there was a viable alternative in .Net Framework. As new versions were released, truly useful functions were added, in addition to removing those that were not.

In 0.6 there was a significant leap: MCART was rewritten in C#. In 0.7 there was another rewrite that brought about a restructuring of the code. Then, in 0.8 I finally decided to publish MCART as a [NuGet](https://www.nuget.org/packages/TheXDS.MCART/) package.

During the focus, projects to support and extend UI frameworks were created and then deprecated. This was because MCART has always been a passion project, not so much one that is commercialized or with which apps for the market or public consumption were created (although, in spirit, that was the idea).

There is a [document](https://github.com/TheXDS/MCART/blob/master/docs/articles/old/CHANGELOG.md) that summarizes some of the changes that occurred in the pre-GIT era of MCART.

## Today
MCART is still under active development. My work as a developer may prevent me from making the progress I would like, but I always try to occasionally open the project, review the code, implement some new functionality, write more unit tests, or simply appreciate the evolution of the project from its genesis.

I hope that one day I can see MCART at the point where I envision it: as a general-purpose library that developers can use to streamline their workflows and provide them with the tools they need to create all kinds of applications, regardless of size.
