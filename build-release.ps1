$compress = @{
  Path = ".\bin\Release\FanControl.Liquidctl.dll", ".\cli.exe", ".\liquidctl-license.txt"
  DestinationPath = ".\FanControl.Liquidctl.zip"
}
Compress-Archive @compress