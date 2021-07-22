# Windows Context Menu Editor

Create & delete Windows context menu with json config file.

## Build Requirement
- .NET Core 5.0

## Tested Platform
- Windows 10 20H2

## Build
```shell
$ dotnet build
```

## Run
Currently create context menu on specific file extension require UAC elevation to write registry under HKEY_CLASS_ROOT.

Run with `dotnet run` is not able to popup a UAC dialog and will throw an exception.

## Command-line Arguments

Create context menu with specific config file.
```shell
$ ./ContextmenuEditor.exe create ./config.json
```

Delete context menu defined in specific config fire.
```shell
$ ./ContextmenuEditor.exe create ./config.json
```

## Config File
See `config-example.json`
```json
{
  "directory": true,
  "directory_background": true,
  "drive": true,
  "file_extensions": [".txt"],
  "key": "TestCtxMenu",
  "command": {
    "name": "Context Menu Test",
    "sub_commands": {
      "Foo": {
        "name": "Command Foo",
        "shell": "Command Foo %1"
      },
      "Bar": {
        "name": "Command Bar",
        "shell": "Command Bar %1"
      },
      "SubMenu": {
        "name": "Sub Menu",
        "sub_commands": {
          "Foo": {
            "name": "Command Foo",
            "shell": "Command Foo %1"
          }
        }
      }
    }
  }
}
```
This will create a context menu on hard-drive, directory, directory background and `*.txt` file like this:
```
-----------------------
| ...                 | ---------------
| Context Menu Test > | | Command Foo |
| ...                 | | Command Bar | ---------------
----------------------- | Sub Menu  > | | Command Foo |
                        --------------- ---------------
                                        
                                        
```