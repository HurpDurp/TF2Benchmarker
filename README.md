# TF2Benchmarker
An Automated Team Fortress 2 Benchmarker

### Setup

To begin, you need:
- A Team Fortress 2 installation.
- **(Optional)** FPS config to benchmark from.
- A list of commands to benchmark.

### Usage

1. Remove any configs that could interfere with the benchmark (usually autoexec.cfg)
2. Set your TF2 path to your `Team Fortress 2` directory (Usually steamapps/common/Team Fortress 2).
3. **(Optional)** Set your DirectX level.
4. Load an existing FPS config or use the default TF2 configuration.
5. Load or add commands to benchmark. The file format for loading benchmarks is described below.
6. **(Optional)** In the `Benchmark List` and `FPS Config` tabs, uncheck values you don't wish to use.
7. **(Optional)** Specify a demo to benchmark.
8. Start the benchmark.

### Benchmark File Format

To quickly add commands to benchmark, you can place your commands in a `.txt` file.
Each line will be parsed for a command, and benchmarked separately.
```
example_command 1
example_command "2"
// Both of these are accepted, comments are automatically ignored
```

If you want to run multiple commands simultaneously, you can do so in the following format:
```
Name to display | example_command 1 | example_command 2 | example_command 3  ...
```

Working examples can be found in the `Benchmark Examples` folder.

### Process

Every time you start a benchmark, it will perform the following steps:

1. Back up any existing `cfg/config.cfg` and `sourcebench.csv` files.
2. Generate a clean config.cfg, using the following:
  * If the `Default` option is selected, the game will be started with the `-default` parameter.
  * If the `Custom Config` option is selected, the current FPS config will be loaded.
3. The game is started, the DirectX level is set (if needed) and the game will benchmark the clean configuration.
4. For each command in the list, an `autoexec.cfg` will be generated.
  * Conflicts between existing commands and the benchmark values will be automatically resolved.
5. Each command is benchmarked, with the game restarting between each command.
6. Results are parsed from the `sourcebench.csv` file, and displayed in the results tab.
7. Backed up files are restored, and temporary files are removed.

### Pitfalls

When benchmarking, it is important to note that there are a number of factors that can skew results.

* Ensure that you close out all applications that could interfere with the benchmark. These include internet browsers, VoIP applications, etc.
* Under normal conditions benchmarking can result in variations of ±2-3 fps between identical runs. Keep this in mind while evaluating results.

* Keeping `-dxlevel` in the launch options can negatively affect performance. Remember to remove it once you've set your DirectX level to the desired value.
