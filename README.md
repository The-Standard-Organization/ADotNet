# ADotNet

![ADotNet](https://raw.githubusercontent.com/The-Standard-Organization/ADotNet/main/ADotNet/ADotNet_git_logo.png)

[![Build](https://github.com/The-Standard-Organization/ADotNet/actions/workflows/build.yml/badge.svg)](https://github.com/The-Standard-Organization/ADotNet/actions/workflows/build.yml)
![Nuget](https://img.shields.io/nuget/v/ADotNet?logo=nuget&color=blue)
![Nuget](https://img.shields.io/nuget/dt/ADotNet?logo=nuget&color=blue&label=Downloads)
[![The Standard](https://img.shields.io/github/v/release/hassanhabib/The-Standard?filter=v2.10.1&style=default&label=Standard%20Version&color=2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard - COMPLIANT](https://img.shields.io/badge/The_Standard-COMPLIANT-2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard Community](https://img.shields.io/discord/934130100008538142?color=%237289da&label=The%20Standard%20Community&logo=Discord)](https://discord.gg/vdPZ7hS52X)

# ADotNet (ADO Dot Net)
ADotNet is a.NET library that enables software engineers on the .NET platform to develop AzureDevOps pipelines and Git Actions in C#.

## Introduction
Today, there's an issue with developing Azure DevOps pipelines and Git Actions with YAML. The technology/language can be challenging to learn and predict the available options for orchestrating build steps. _ADotNet_ presents a solution to pipeline tasks as predefined C # models, with all the options available to orchestrate a pipeline without searching for the available options on the documentation websites.

## Standard-Compliance
This library was built according to The Standard, which recommends that it follow engineering principles, patterns, and tooling.

This library is also a community effort, which involved many nights of pair programming, test-driven development, and in-depth exploration research and design discussions.

## Standard-Promise
A Standard complaint system's most crucial fulfillment aspect is contributing to people, its evolution, and its principles.
An organization that systematically honors an environment of learning, training, and sharing knowledge is an organization that learns from the past, makes calculated risks for the future, 
and brings everyone within it up to speed on the current state of things as honestly, rapidly, and efficiently as possible. 
 
We believe that everyone has the right to privacy and will never do anything that could violate that right.
We are committed to writing ethical and responsible software and will always strive to use our skills, coding, and systems for the good.
These beliefs will help ensure that our software(s) are safe and secure and that it will never be used to harm or collect personal data for malicious purposes.
 
As a promise to you, the Standard Community upholds these values.

## How It Works for AzureDevOps
Here's how this library works. Let's assume you want to write a task in your pipeline that restores packages for your ASP.NET Core project. Today, engineers write the following Command in YAML:

```yaml
- task: DotNetCoreCLI@2
  displayName: 'Restore'
  inputs:
    command: 'restore'
    feedsToUse: 'select'
```

The problem with the above YAML code is that it takes more work to remember. Even while staring at it, I can't seem to remember `DotNetCoreCLI@2` and what this means to someone who is a full-stack engineer trying to get off the ground as fast as possible. Here's how the very same code above would look like in ADotNet:

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

The options here are available with the power of strongly typed options and Enums. You don't have to think about what needs to go there. The syntax already directs you to the options you need to start building your pipeline.

## How It Works for Git Actions
Here's how this library works. Let's assume you want to write a task in your pipeline that uses a particular version for your ASP.NET Core project. Today, engineers write the following command in YAML:

```yaml
- name: Setup .Net
    uses: actions/setup-dotnet@v1
    with:
      dotnet-version: 6.0.100-rc.1.21463.6
      include-prerelease: true
```

The problem with the above YAML code is that it takes more work to remember. Even while I'm staring at it, I can't seem to remember `actions/setup-dotnet@v1` and what does this mean to someone who is a full-stack engineer trying to get off the ground as soon as possible? Here's how the very same code above would look like in ADotNet:

```csharp
  new SetupDotNetTaskV1
  {
      Name = "Setup .Net",

      TargetDotNetVersion = new TargetDotNetVersion
      {
          DotNetVersion = "6.0.100-rc.1.21463.6",
          IncludePrerelease = true
      }
  }
```

The options here are available with the power of strongly typed options and Enums. You don't have to think about what needs to go there. It's already directing you towards the options you need to get going with building your pipeline.

## Dependencies & Kudos
This library relies heavily on [YamlDotNet](https://github.com/aaubry/YamlDotNet), an amazing .NET library developed by [Antoine Aubry](https://github.com/aaubry) and many other amazing contributors who made C# to YAML possible.

The library also leverages native .NET `System.IO.File` functionality to write files to a destination of the consumer's choosing.

## The Architecture
The library's architecture follows [The Standard](https://github.com/hassanhabib/The-Standard). Breaking its capabilities into brokers, services, and clients. Here's a low-level architecture view of how it works:

<p align="center">
  <img src="https://user-images.githubusercontent.com/89320816/137257287-b4b864a0-312e-4f86-b9c0-e436aeddaef6.png">
</p>

The abstraction of the dependencies should allow for easy future expansion and pluggability for any other C# to YAML components.

## Real-Life Example for AzureDevOps
Here's something I'm using in my open source [OtripleS](https://github.com/hassanhabib/OtripleS) project, which is built in ASP.NET Core 6.0:

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

## Real-Life Example for Git Actions
Here's something I'm using in my open source [OtripleS](https://github.com/hassanhabib/OtripleS) project, which is built in ASP.NET Core 6.0:

```csharp
  var aDotNetClient = new ADotNetClient();

  var githubPipeline = new GithubPipeline
  {
      Name = "Github",

      OnEvents = new Events
      {
          Push = new PushEvent
          {
              Branches = new string[] { "master" }
          },

          PullRequest = new PullRequestEvent
          {
              Branches = new string[] { "master" }
          }
      },

      Jobs = new Dictionary<string, Job>
      {
          {
              "build",
              new Job
              {
                  RunsOn = BuildMachines.Windows2019,

                  Steps = new List<GithubTask>
                  {
                      new CheckoutTaskV2
                      {
                          Name = "Check out"
                      },

                      new SetupDotNetTaskV1
                      {
                          Name = "Setup .Net",

                          TargetDotNetVersion = new TargetDotNetVersion
                          {
                              DotNetVersion = "6.0.100-rc.1.21463.6",
                              IncludePrerelease = true
                          }
                      },

                      new RestoreTask
                      {
                          Name = "Restore"
                      },

                      new DotNetBuildTask
                      {
                          Name = "Build"
                      },

                      new TestTask
                      {
                          Name = "Test"
                      }
                  }
              }
          }
      }
  };

  string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
  string directoryPath = Path.GetDirectoryName(buildScriptPath);

  if (!Directory.Exists(directoryPath))
  {
      Directory.CreateDirectory(directoryPath);
  }

  aDotNetClient.SerializeAndWriteToFile(githubPipeline, path: buildScriptPath);
```

And here's the YAML output of this code:

```yaml
name: Github
on:
  push:
    branches:
    - master
  pull_request:
    branches:
    - master
jobs:
  build:
    runs-on: windows-2019
    steps:
    - name: Check out
      uses: actions/checkout@v2
    - name: Setup .Net
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 6.0.100-rc.1.21463.6
        include-prerelease: true
    - name: Restore
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal

```

And finally, here's the result:

![image](https://user-images.githubusercontent.com/89320816/137255979-8e15772e-f3f7-48e3-bc57-8b6cf32c7a09.png)

## Some Odd Decisions
I have intentionally limited some of the capabilities in this library to ensure any contributions go to this repository so everyone can benefit from the updates. For instance, I could have easily made selecting a virtual machine(VM) as a string input to allow anyone to pass in whatever VM they need. But the problem with that is for those needing the same thing and having to do the same research to find the right VM for their build.

I'm intentionally making my library less usable to ensure this level of hive mindset is reflected in our changes here.

If you have any suggestions, comments, or questions, please feel free to contact me at:

[Twitter](https://twitter.com/hassanrezkhabib)

[LinkedIn](https://www.linkedin.com/in/hassanrezkhabib/)

[E-Mail](mailto:hassanhabib@live.com)

### Important Notice
I want to give special thanks to all the contributors who made this project a success, especially Mr. Hassan Habib and Mr. Christo du Toit for their dedicated contributions.
