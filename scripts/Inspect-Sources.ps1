$ErrorActionPreference = 'Stop'
$workspace = Split-Path $PSScriptRoot -Parent
Set-Location -LiteralPath $workspace
New-Item -ItemType Directory -Force '.reference/text' | Out-Null
function Clean([string]$html) {
    $text = [Net.WebUtility]::HtmlDecode(($html -replace '(?s)<[^>]+>', ' '))
    return ($text -replace '\s+', ' ').Trim()
}
foreach ($file in Get-ChildItem '.reference/website_*.html') {
    $html = Get-Content $file.FullName -Raw -Encoding utf8
    $blocks = [regex]::Matches($html, '(?s)<(h[1-6]|p|li)\b[^>]*>(.*?)</\1>')
    $paragraphs = foreach ($block in $blocks) {
        # Navigation lists are not content. Paragraphs and article headings are retained.
        if ($block.Value -match 'menu-item') { continue }
        $text = Clean $block.Groups[2].Value
        if ($text -and $text -notmatch "^We're not around") { $text }
    }
    $paragraphs | Set-Content ".reference/text/$($file.BaseName).txt" -Encoding utf8
}
$legacy = '.reference/legacy/DiveHub-Android-Legacy-main'
foreach ($file in Get-ChildItem "$legacy/res/layout/*.xml") {
    [xml]$xml = Get-Content $file.FullName -Raw -Encoding utf8
    $ns = New-Object Xml.XmlNamespaceManager($xml.NameTable)
    $ns.AddNamespace('android', 'http://schemas.android.com/apk/res/android')
    $text = $xml.SelectNodes('//*[@android:text]', $ns) | ForEach-Object { $_.GetAttribute('text', 'http://schemas.android.com/apk/res/android') }
    $text | Set-Content ".reference/text/legacy_$($file.BaseName).txt" -Encoding utf8
}
# Read every Java/XML source locally. Do not print or migrate the old SMTP implementation.
$files = Get-ChildItem $legacy -Recurse -File | Where-Object { $_.Extension -in '.java','.xml' }
$inventory = foreach ($file in $files) {
    $text = Get-Content $file.FullName -Raw
    [ordered]@{ Path=$file.FullName.Substring((Resolve-Path $legacy).Path.Length+1); Bytes=$file.Length; Lines=($text -split "`n").Count }
}
$inventory | ConvertTo-Json | Set-Content '.reference/source_inventory.json' -Encoding utf8
Write-Output "Inspected $($files.Count) Java/XML source files. Extracted content into .reference/text."
