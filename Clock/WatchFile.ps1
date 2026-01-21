$configFile = "C:\Users\Admin\source\repos\WinClock\Clock\alarmWakeUp.ini"
$exePath = "C:\Users\Admin\source\repos\WinClock\Clock\bin\Debug\Clock.exe"
$taskName = "MyAlarmTask"
$logFile = "$env:TEMP\WatchFile.log"


# Функция: обновить задачу в Планировщике
function Update-ScheduledTask {
    # Читаем время из файла
    if (-not (Test-Path $configFile)) {
        Add-Content -Path $logFile -Value "[$(Get-Date)] ОШИБКА: Файл $configFile не найден" -Encoding UTF8
        return
    }

    $alarmTimeStr = (Get-Content $configFile -First 1).Trim()
    if ([string]::IsNullOrWhiteSpace($alarmTimeStr)) {
        Add-Content -Path $logFile -Value "[$(Get-Date)] ОШИБКА: Пустая строка в $configFile" -Encoding UTF8
        return
    }

    # Конвертируем в DateTime
    try {
        $alarmTime = [datetime]::ParseExact(
            $alarmTimeStr,
            "yyyy-MM-dd HH:mm:ss",
            [System.Globalization.CultureInfo]::InvariantCulture
        )
    }
    catch {
        Add-Content -Path $logFile -Value "[$(Get-Date)] ОШИБКА: Неверный формат даты '$alarmTimeStr'" -Encoding UTF8
        return
    }

    # Проверяем, не прошло ли время
    $now = Get-Date
    if ($alarmTime -lt $now) {
        Add-Content -Path $logFile -Value "[$(Get-Date)] ВРЕМЯ ПРОШЛО: $alarmTime (сейчас: $now)" -Encoding UTF8
        return
    }

    # Проверяем, запущен ли процесс
    $processName = (Split-Path $exePath -Leaf).Replace(".exe", "")
    if (Get-Process -Name $processName -ErrorAction SilentlyContinue) {
        Add-Content -Path $logFile -Value "[$(Get-Date)] $processName уже работает" -Encoding UTF8
        return
    }

    # Создаём триггер (однократный запуск)
    $trigger = New-ScheduledTaskTrigger -At $alarmTime -Once


    # Создаём действие
    $action = New-ScheduledTaskAction -Execute $exePath


    # Настройки: пробуждение из сна
    $settings = New-ScheduledTaskSettingsSet -WakeToRun


    # Удаляем существующую задачу (если есть)
    Get-ScheduledTask -TaskName $taskName -ErrorAction SilentlyContinue | Unregister-ScheduledTask -Confirm:$false


    # Регистрируем новую задачу
    try {
        Register-ScheduledTask `
            -TaskName $taskName `
            -Trigger $trigger `
            -Action $action `
            -Settings $settings `
            -RunLevel Highest `
            -Force | Out-Null
        Add-Content -Path $logFile -Value "[$(Get-Date)] Задача обновлена: запуск в $alarmTime" -Encoding UTF8
    }
    catch {
        Add-Content -Path $logFile -Value "[$(Get-Date)] ОШИБКА при регистрации задачи: $_" -Encoding UTF8
    }
}

# Создаём наблюдатель за файлом
$watcher = New-Object System.IO.FileSystemWatcher
$watcher.Path = [System.IO.Path]::GetDirectoryName($configFile)
$watcher.Filter = [System.IO.Path]::GetFileName($configFile)
$watcher.NotifyFilter = [System.IO.NotifyFilters]::LastWrite
$watcher.EnableRaisingEvents = $true


# Действие при изменении файла
$action = {
    Add-Content -Path $logFile -Value "[$(Get-Date)] Обнаружено изменение $configFile" -Encoding UTF8
    Update-ScheduledTask
}


# Регистрируем событие
Register-ObjectEvent $watcher "Changed" -Action $action


# Держим скрипт работающим (24 часа)
Start-Sleep -Seconds 86400