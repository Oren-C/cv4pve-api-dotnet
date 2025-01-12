# Corsinvest.ProxmoxVE.Api

[![License](https://img.shields.io/github/license/Corsinvest/cv4pve-api-dotnet.svg)](LICENSE.md) ![Nuget](https://img.shields.io/nuget/v/Corsinvest.ProxmoxVE.Api.svg?label=Nuget%20%20Api) ![Nuget](https://img.shields.io/nuget/v/Corsinvest.ProxmoxVE.Api.Extension.svg?label=Nuget%20%20Extension) ![Nuget](https://img.shields.io/nuget/v/Corsinvest.ProxmoxVE.Api.Metadata.svg?label=Nuget%20%20Metadata) ![Nuget](https://img.shields.io/nuget/v/Corsinvest.ProxmoxVE.Api.Shell.svg?label=Nuget%20%20Shell)

Proxmox VE Client API .Net

[Proxmox VE Api](https://pve.proxmox.com/pve-docs/api-viewer/)

[Nuget Api](https://www.nuget.org/packages/Corsinvest.ProxmoxVE.Api)

[Nuget Extension](https://www.nuget.org/packages/Corsinvest.ProxmoxVE.Api.Extension)

[Nuget Metadata](https://www.nuget.org/packages/Corsinvest.ProxmoxVE.Api.Metadata)

[Nuget Shell](https://www.nuget.org/packages/Corsinvest.ProxmoxVE.Api.Shell)

```text
   ______                _                      __
  / ____/___  __________(_)___ _   _____  _____/ /_
 / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
/ /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
\____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/

Corsinvest for Proxmox VE Api Client  (Made in Italy)
```

## Copyright and License

Copyright: Corsinvest Srl
For licensing details please visit [LICENSE.md](LICENSE.md)

## Commercial Support

This software is part of a suite of tools called cv4pve-tools. If you want commercial support, visit the [site](https://www.cv4pve-tools.com)

## General

The client is generated from a JSON Api on Proxmox VE.

## Main features

* Easy to learn
* Method named
* Method native suffix Rest (prevent)
  * GetRest
  * CreateRest (Post)
  * SetRest (Put)
  * DeleteRest
* Set ResponseType json, png
* Full class and method generated from documentation (about client)
* Comment any method and parameters
* Parameters indexed eg [n] is structured in array index and value
* Tree structure
  * client.Nodes["pve1"].Qemu[100].Snapshot().snapshotList().Response.data
* Return data Proxmox VE
* Debug Level show to console information REST call
* Return result
  * Request
  * Response
  * Status
* Last result action
* Task utility
  * WaitForTaskToFinish
  * TaskIsRunning
  * GetExitStatusTask
* Method direct access
  * Get
  * Create (Post)
  * Set (Put)
  * Delete
* Login return bool if access
* Return Result class more information
* ClientBase lite function
* Form Proxmox VE 6.2 support Api Token for user
* Async/Await

## Api token

From version 6.2 of Proxmox VE is possible to use [Api token](https://pve.proxmox.com/pve-docs/pveum-plain.html).
This feature permit execute Api without using user and password.
If using **Privilege Separation** when create api token remember specify in permission.
Format USER@REALM!TOKENID=UUID

## Result

The result is class **Result** and contain properties:

* **Response** returned from Proxmox VE (data,errors,...) dynamic [ExpandoObject](https://msdn.microsoft.com/en-US/library/system.dynamic.expandoobject(v=vs.110).aspx)
* **ResponseToDictionary** return response to dictionary ```IDictionary<String, object>```
* **ResponseInError** (bool) : Contains errors from Proxmox VE.
* **StatusCode** (System.Net.HttpStatusCode): Status code of the HTTP response.
* **ReasonPhrase** (string): The reason phrase which typically is sent by servers together with the status code.
* **IsSuccessStatusCode** (bool) : Gets a value that indicates if the HTTP response was successful.
* **GetError()** (string) : Get error.

Example result:

```json
{
  "data": {
    "smbios1": "uuid=9246585e-0c8b-4d02-8fe2-f48fd0da3975",
    "ide2": "none,media=cdrom",
    "onboot": 1,
    "boot": "cdn",
    "cores": 2,
    "agent": 1,
    "memory": 4096,
    "numa": 0,
    "bootdisk": "virtio0",
    "sockets": 1,
    "net0": "virtio=3A:39:38:30:36:31,bridge=vmbr0",
    "parent": "auto4hours170904080002",
    "digest": "acafde32daab50bce801fef2e029440c54ebe2f7",
    "vga": "qxl",
    "virtio0": "local-zfs:vm-100-disk-1,cache=writeback,size=50G",
    "ostype": "win8",
    "name": "phenometa"
  }
}
```

## Usage

```C#
//if you want use lite version only get/set/create/delete use PveClientBase

var client = new PveClient("10.92.90.91");
if (await client.Login("root", "password"))
{
    var vm = await client.Nodes["pve1"].Qemu[100];

    //config vm
    var config = await vm.Config.VmConfig();
    Console.WriteLine(JsonConvert.SerializeObject(config.Response,Formatting.Indented));

    //create snapshot
    var response = await vm.Snapshot.Snapshot("pippo2311");

    //update snapshot description
    await vm.Snapshot["pippo2311"].Config.UpdateSnapshotConfig("description");

    //delete snapshot
    await vm.Snapshot["pippo2311"].Delsnapshot();

    //list of snapshot
    foreach (var snapshot in (await vm.Snapshot.SnapshotList()).Response.data)
    {
        Console.WriteLine(JsonConvert.SerializeObject(snapshot,Formatting.Indented));
        Console.WriteLine(snapshot.name);
    }

    //change response type from json to png
    client.ResponseType = "png";
    var dataImg = client.Nodes["pve1"].Rrd.Rrd("cpu", "day").Response;
    Console.WriteLine("<img src=\"{dataImg}\" \>");
}

//using Api Token
var client = new PveClient("10.92.100.33");
client.ApiToken = "root@pam!qqqqqq=8a8c1cd4-d373-43f1-b366-05ce4cb8061f";
var version = await client.Version.Version();
Console.WriteLine(JsonConvert.SerializeObject(version.Response.data, Formatting.Indented));
```
