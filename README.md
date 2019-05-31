# BeepBong

> _This is a code respository, and here are the headlines..._

## What is BeepBong

BeepBong is a database to help catalog Audio Tracks and Samples for Television and Radio programmes. The platform is designed to be a repository for audio track metadata _(et al [MusicBrainz](https://musicbrainz.org) for Albums)_

![Broadcaster Page](https://i.imgur.com/EO9IypYl.png)

![Programme Detail](https://i.imgur.com/StBNLHVl.png)

![Sample Upload](https://i.imgur.com/QmwMz7ll.png)

## Starting a BeepBong catalog

BeepBong currently does not have any official releases, or processes of managing users. The best way to start creating a catalog is to create your own instance.

#### Creating a Instance

First you need to retrieve the code before launching an instance and creating a database.

```
git clone https://github.com/Jimmyson/BeepBong.git
```

Once the code is downloaded, you can commence a build of the plaform.

```
dotnet ef database update .\src\BeepBong.DataAccess --startup-project .\src\BeepBong.Web
dotnet build .\src\BeepBong.Web
```

#### Retrieving JavaScript libraries for the Web UI

BeepBong utilises the Library Manager to handel the JavaScript libraries

```
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```

Once the library manager is installed, the libraries can now be fetched

```
libman restore
```

#### Starting the Web Instance

You are now ready to start the instance, and add data to the database

```
dotnet run .\src\BeepBong.Web
```
