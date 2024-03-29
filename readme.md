# YAL: Yet Another Language

YAL is a language which natively supports concurrent tasks through *async/await* keywords for microcontrollers (such as the ESP32 or Arduino). YAL source code can be transpiled to C++ through the provided compiler, or scaffold a PlatformIO project to "easily" flash onto the device. There are 2 ways to compile yal, either singlefile or as a project, both support emulating the code using wokwi.com 

### Single file compile

**Step 1:**  
Clone the Github repository.

**Step 2:**  
Optionally view/edit the sample source code in `./program.yal`.  

**Step 3:**  
Run the build command to compile the compiler:  
**Windows:** `$ dotnet build .\YALCompiler\YALCompiler\YALCompiler.csproj --configuration Release`  
**MacOS:** `dotnet build ./YALCompiler/YALCompiler/YALCompiler.csproj --configuration Release`

**Step 4:**  
Compile the sample code to C++:  
**Windows:** `$ .\YALCompiler\YALCompiler\bin\Release\net7.0\YALCompiler.exe compile -i program.yal -o output.cpp`  
**MacOS:** `./YALCompiler/YALCompiler/bin/Release/net7.0/YALCompiler compile -i program.yal -o output.cpp`  
To run in a simulator automatically `-w <wokwi_url>` can be used, recomended url to use with test is https://wokwi.com/projects/new/esp32

**Step 5:**
The generated output must manually be copied to into platformIO project to be compiled.


The compiled C++ code can be found in `./output.cpp`

### System Requirements
- .NET 7 SDK

## Single file compile and run (simulator)

**Step 1:**  
(Assuming the repository has been cloned and built) Run the following command to build the YAL program source (the example uses `./program.yal`) to a C++ file (`/output.cpp`) and run the simulator.  
**Windows:** `$ .\YALCompiler\YALCompiler\bin\Release\net7.0\YALCompiler.exe compile -i program.yal -o output.cpp -w https://wokwi.com/projects/new/esp32`  
**MacOS:** `./YALCompiler/YALCompiler/bin/Release/net7.0/YALCompiler compile -i program.yal -o output.cpp -w https://wokwi.com/projects/new/esp32`  
This example uses the default Wokwi simulator for ESP32. The first build may take a while to set up the required (Puppeteer) dependencies.

### System Requirements
- .NET 7 SDK

## Project Scaffolding

**Step 1:**  
(Assuming the repository has been cloned and built) Initialize your new project by running the following:  
**Windows:** `$ .\YALCompiler\YALCompiler\bin\Release\net7.0\YALCompiler.exe project -i`  
**MacOS:** `./YALCompiler/YALCompiler/bin/Release/net7.0/YALCompiler project -i`  
This will create a project in your current folder, along with the required folders/files to build.
To specify a board use `-b <board`
To specify another folder at the project folder use `-d <path_to_directory>`
If your installation of PlatformIO cannot be found, it may be necessary to add `-p <path_to_PIO_CLI>` to the command, if this is the case it should be done for everystep in complation.

**Step 2:**
To get get a flashable file use:  
**Windows:** `$ .\YALCompiler\YALCompiler\bin\Release\net7.0\YALCompiler.exe project -c`  
**MacOS:** `./YALCompiler/YALCompiler/bin/Release/net7.0/YALCompiler project -c`  
this will create a .elf file in pioBuild/.pio/<platform_name>/firmware.elf
platform name is defined by the list maintained by platfromIO which can be found [here](https://docs.platformio.org/en/latest/boards/index.html), or use pio --boards. Currently only expressif is tested 

**Step 3:**
To flash the file you may either use the pio cli to flash or altertivly open the pioBuild folder with vscode and the pio plugin.

To run in a simulator automatically `-r -w <wokwi_url>` can be used, recomended url to use with test is https://wokwi.com/projects/new/esp32


### System Requirements
- .NET 7 SDK
- PlatformIO CLI
