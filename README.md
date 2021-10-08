# ADotNet
<p align="center">
  <img width="25%" height="25%" src="https://github.com/hassanhabib/ADotNet/blob/master/ADotNet/ADotNet.png">
</p>

[![.NET](https://github.com/hassanhabib/ADotNet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hassanhabib/ADotNet/actions/workflows/dotnet.yml)
![Nuget](https://img.shields.io/nuget/v/ADotNet)

# ADotNet (ADO Dot Net)
ADotNet is a.NET library that enables software engineers on the .NET platform to develop AzureDevOps pipelines in C#.

## Introduction
There's an issue today with developing Azure DevOps pipelines with YAML. The technology/language can be quite challenging to learn and predict what the available options are when it comes to orchestrating build steps.

ADotNet presents a solution to pipeline tasks as C# models. Predefined, with all the options available to orchestrate a pipeline without having to search for the available options on the documentation websites.

## How It Works
Here's how this library works. Let's assume you want to write a task in your pipeline that restores packages for your ASP.NET Core project. Today, engineers write the following command in YAML:

```yaml
- task: DotNetCoreCLI@2
  displayName: 'Restore'
  inputs:
    command: 'restore'
    feedsToUse: 'select'
```

The problem with the above YAML code is that it's not that easy to remember. Even while I'm starting at it right now, I just can't seem to remember `DotNetCoreCLI@2` and what does all of this means to someone who is a full-stack engineer trying to get off the ground as soon, easy and as fast as possible? Here's how the very same code above would look like in ADotNet:

```csharp
  new DotNetExecutionTask
  {
      DisplayName = "Restore",

      Inputs = new DotNetExecutionTasksInputs
      {
          Command = Command.restore,
          FeedsToUse = Feeds.select
      }
  }
```

The options here are available with the power of strongly typed options and Enums. You don't have to think about what needs to go there. It's already directing you towards the options you need to get going with building your pipeline.

## Dependencies & Kudos
This library relies heavily on [YamlDotNet](https://github.com/aaubry/YamlDotNet) which is an amazing .NET library developed by [Antoine Aubry](https://github.com/aaubry) along with so many other amazing contributors who made C# to YAML possible.

The library also leverages native .NET `System.IO.File` functionality to write files to a destination of the consumer's choosing.

## The Architecture
The library's architecture follows [The Standard](https://github.com/hassanhabib/The-Standard). Breaking it's capabilities into brokers, services and clients. Here's a low-level architecture view of how it works:

<p align="center">
  <img src="https://user-images.githubusercontent.com/1453985/124557013-d8d8b300-dded-11eb-8fa0-4805a7e5259b.png">
</p>

The abstraction of the dependencies should allow a future expansion and pluggability for any other C# to YAML components easily.

## Real-Life Example
Here's something I'm using in my open source [OtripleS](https://github.com/hassanhabib/OtripleS) project which is built in ASP.NET Core 6.0:

```csharp
  var adoClient = new ADotNetClient();

  var aspNetPipeline = new AspNetPipeline
  {
      TriggeringBranches = new List<string>
      {
          "master"
      },

      VirtualMachinesPool = new VirtualMachinesPool
      {
          VirtualMachineImage = VirtualMachineImages.Windows2019
      },

      ConfigurationVariables = new ConfigurationVariables
      {
          BuildConfiguration = BuildConfiguration.Release
      },

      Tasks = new List<BuildTask>
      {
          new UseDotNetTask
          {
              DisplayName = "Use DotNet 6.0",

              Inputs = new UseDotNetTasksInputs
              {
                  Version = "6.0.100-preview.3.21202.5",
                  IncludePreviewVersions = true,
                  PackageType = PackageType.sdk
              }
          },

          new DotNetExecutionTask
          {
              DisplayName = "Restore",

              Inputs = new DotNetExecutionTasksInputs
              {
                  Command = Command.restore,
                  FeedsToUse = Feeds.select
              }
          },

          new DotNetExecutionTask
          {
              DisplayName = "Build",

              Inputs = new DotNetExecutionTasksInputs
              {
                  Command = Command.build,
              }
          },

          new DotNetExecutionTask
          {
              DisplayName = "Test",

              Inputs = new DotNetExecutionTasksInputs
              {
                  Command = Command.test,
                  Projects = "**/*Unit*.csproj"
              }
          },

          new DotNetExecutionTask
          {
              DisplayName = "Publish",

              Inputs = new DotNetExecutionTasksInputs
              {
                  Command = Command.publish,
                  PublishWebProjects = true
              }
          }
      }
  };

  adoClient.SerializeAndWriteToFile(aspNetPipeline, "../../azure-pipelines.yaml");

```

And here's the YAML output of this code:

```yaml
trigger:
- master
pool:
  vmImage: 'ubuntu-latest'
variables:
  buildConfiguration: 'Release'
steps:
- task: UseDotNet@2
  displayName: 'Use DotNet 6.0'
  inputs:
    packageType: 'sdk'
    version: '6.0.100-preview.3.21202.5'
    includePreviewVersions: true
- task: DotNetCoreCLI@2
  displayName: 'Restore'
  inputs:
    command: 'restore'
    feedsToUse: 'select'
- task: DotNetCoreCLI@2
  displayName: 'Build'
  inputs:
    command: 'build'
- task: DotNetCoreCLI@2
  displayName: 'Test'
  inputs:
    command: 'test'
    projects: '**/*Unit*.csproj'
- task: DotNetCoreCLI@2
  displayName: 'Publish'
  inputs:
    command: 'publish'
    publishWebProjects: true

```

And finally, here's the result:

![image](https://user-images.githubusercontent.com/1453985/124557686-a24f6800-ddee-11eb-9bdd-f2d576ddd5c2.png)


## Some Odd Decisions
I have intentionally limited some of the capabilities in this library to ensure any contributions go to this repository so everyone can benefit from the updates. For instance, I could've easily made selecting a virtual machine as a string input to allow for anyone to pass in whatever vm they need. But the problem with that is for those who will need the same thing and have to do the same research to find the right VM for their build.

I'm intentionally making my library less usable to ensure this level of hive mindset is reflected in our changes in here.

If you have any suggestions, comments or questions, please feel free to contact me on:
<br />
Twitter: @hassanrezkhabib
<br />
LinkedIn: hassanrezkhabib
<br />
E-Mail: hassanhabib@live.com
<br />
