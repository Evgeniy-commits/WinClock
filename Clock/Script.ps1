
$alarmTime = Get-Content "C:\Users\Admin\source\repos\WinClock\Clock\Alarm.ini" | Select-Object -First 1
$trigger = New-ScheduledTaskTrigger -At $alarmTime
$action = New-ScheduledTaskAction -Execute "C:\Users\Admin\source\repos\WinClock\Clock\bin\Debug\Clock.exe"
Set-ScheduledTask -TaskName "MyAlarmTask" -Trigger $trigger -Action $action -WakeToRun $true