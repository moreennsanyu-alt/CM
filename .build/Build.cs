            DotNetRun(s => s
                .SetConfiguration(Configuration.Debug)
                .SetProcessEnvironmentVariable("DOTNET_CLI_UI_LANGUAGE", "en-US")
                .SetProcessEnvironmentVariable("NUNIT_RESULTS_DIRECTORY", TestResultsDirectory)
                .EnableNoBuild()
                .CombineWith(
                    testCombinations,
                    (settings, v) => settings
                        .SetProjectFile(v.project)
                        .SetFramework(v.framework)
                        .SetProperty("RunWorkingDirectory", ArtifactsDirectory / "bin" / "ClinicManager.Win" / "debug_win-x64" )
						.SetProcessAdditionalArguments(
                            "--",
							"--coverage",
							"--coverage-output-format cobertura",
							$"--coverage-output {CoverageDirectory / $"{v.project.Name}_{v.framework}.cobertura.xml"}",
                            $"--results-directory {TestResultsDirectory}"
                         )
                    )
                );
