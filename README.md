# MoM (Mother of Modules)
[![Gitter](https://badges.gitter.im/momcms/MoM.svg)](https://gitter.im/momcms/MoM?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
<p><img src="assets/momlogo.png" srcset="assets/momlogo.svg 1x" class="navbar-brand" alt="MoM Logo" width="152" height="150"></p>

### MoM loves all of her children.

## Feature list:

 * .NET Core (1.0.0-rc2-final)
 * MVC 6
 * Uses Depency Injection to create a modular pattern
 * Provides a starting point to create your own solutions
 * Integrates Angular 2 to create a basic SPA CMS
 * Themes are build on Bootstrap 4 using Sass
 * Gulp and NPM are also favored

## Flawor
This solution is most likely not for everyone.
It favors developers who wants to work with MVC 6, EF7, MS SQL Server and Angular 2 as SPA.
The design is meant to give the developer less options on how to build their web application.
On the plus side this should me a more unified style in the modules beeing created.
The file structure and reason why the modules are located in a seperate git project is to make it easy for you to create your own modules project on your github and you can chose to have that private if you like.

## Prerequisities
 * Microsoft Visual Studio 2015 ([Community Edition](https://www.visualstudio.com/en-us) will work fine)
 * Microsoft SQL Server 2014 or higher ([Developer Edition](https://blogs.technet.microsoft.com/dataplatforminsider/2016/03/31/microsoft-sql-server-developer-edition-is-now-free/) is now free)
 * .NET Core ([1.0.0-rc2-final](https://www.microsoft.com/net/core#windows))
 * Git, Node and NPM

## Getting Started
Please go to [MoM.Dev](https://github.com/RolfVeinoeSorensen/MoM.Dev) and follow the instructions there.

### Installing
For now you will need to be a developer and follow the instructions under Getting Started.
MoM will get an installer to make it alot easier at a later time so stay tuned.

### Repositories
 * [MoM.Dev](https://github.com/RolfVeinoeSorensen/MoM.Dev)
 * [MoM.Modules](https://github.com/RolfVeinoeSorensen/MoM.Modules)
 * [MoM.Themes](https://github.com/RolfVeinoeSorensen/MoM.Themes)

## Goal
Mother of Modules is meant to be easy for end users to install and maintain.
MoM will focus on usability and becomming a good choice for businesses.

## This solution is not ready for production sites
I am currently working on making this solution ready enough to serve my personal website.
It will in time feature the possibility to load modules dynamically like Orchard 2 allready does.
Wiki and tutorials are still to come.

## Authors

* **Rolf Veinø Sørensen** - *Initial work* - [easymodules.net](https://easymodules.net/)

See also the list of [contributors](https://github.com/RolfVeinoeSorensen/MoM/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments
 * [Orchard 2](https://github.com/OrchardCMS/Orchard2) Very cool Free, open source CMS, but more complex and generic.
 * [ExtCore](https://github.com/ExtCore/ExtCore) Free, open source and cross-platform framework for creating modular and extendable web applications based on ASP.NET Core 1.0.