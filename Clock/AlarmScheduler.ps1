
$configFile = "C:\Users\Admin\source\repos\WinClock\Clock\alarmWakeUp.ini"
$exePath = "C:\Users\Admin\source\repos\WinClock\Clock\bin\Debug\Clock.exe"
$taskName = "AlarmTask"

# Читаем время из файла (первая строка)
if (-not (Test-Path $configFile)) {
    Write-Output "Файл конфигурации не найден: $configFile"
    exit 1
}

$alarmTimeStr = (Get-Content $configFile -First 1).Trim()
if ([string]::IsNullOrWhiteSpace($alarmTimeStr)) {
    Write-Output "Пустая строка в файле конфигурации"
    exit 1
}

# Конвертируем строку в DateTime
try {
    $alarmTime = [datetime]::ParseExact(
        $alarmTimeStr,
        "yyyy-MM-dd HH:mm:ss",  # Формат: 2026-01-22 08:30:00
        [System.Globalization.CultureInfo]::InvariantCulture
    )
}
catch {
    Write-Output "Ошибка формата даты в файле: $alarmTimeStr"
    exit 1
}
# Вычитаем 1 минуту
    $alarmTime = $alarmTime.AddMinutes(-1)

# Проверяем, не прошло ли время
$now = Get-Date
if ($alarmTime -lt $now) {
    Write-Output "Время уже прошло: $alarmTime (сейчас: $now)"
    exit 0
}

# Проверяем, запущен ли процесс
$processName = (Split-Path $exePath -Leaf).Replace(".exe", "")
if (Get-Process -Name $processName -ErrorAction SilentlyContinue) {
    Write-Output "$processName уже работает"
    exit 0
}

# Создаём триггер (однократный запуск)
$trigger = New-ScheduledTaskTrigger -At $alarmTime -Once


# Создаём действие
$action = New-ScheduledTaskAction -Execute $exePath


# Создаём настройки с флагом WakeToRun
$settings = New-ScheduledTaskSettingsSet -WakeToRun


# Удаляем существующую задачу (если есть)
Get-ScheduledTask -TaskName $taskName -ErrorAction SilentlyContinue | Unregister-ScheduledTask -Confirm:$false


# Регистрируем новую задачу
Register-ScheduledTask `
    -TaskName $taskName `
    -Trigger $trigger `
    -Action $action `
    -Settings $settings `
    -RunLevel Highest `
    -Force | Out-Null


Write-Output "Задача обновлена: запуск в $alarmTime (пробуждение из сна включено)"