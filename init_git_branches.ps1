#Requires -Version 5.0
# Creates branches: main (short README), pr4 (full project). Run from pit folder.
#   powershell -ExecutionPolicy Bypass -File .\init_git_branches.ps1
$ErrorActionPreference = "Continue"
Set-Location $PSScriptRoot

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    throw "Install Git for Windows first."
}

if (-not (Test-Path "README.main.md") -or -not (Test-Path "README.pr4.md")) {
    throw "README.main.md and README.pr4.md must exist."
}

if (-not (Test-Path ".git")) {
    git init
}

Copy-Item -LiteralPath "README.main.md" -Destination "README.md" -Force
git add -- "README.md" "README.main.md" ".gitignore" ".gitattributes"
$st = git status --porcelain
if ($st) {
    git commit -m "repo: root readme; PR4 materials in branch pr4"
}
git branch -M main

git checkout -b pr4 2>$null
if ($LASTEXITCODE -ne 0) {
    git checkout pr4
}
Copy-Item -LiteralPath "README.pr4.md" -Destination "README.md" -Force
git add --all
git add -- "README.md" "README.pr4.md" "README.main.md" "init_git_branches.ps1"
git commit -m "pr4: WPF .NET practice 4, variant 14, screenshots" 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "pr4 commit skipped (nothing new or already committed)."
}

Write-Host ""
Write-Host "Done. Branch main = short readme. Branch pr4 = full project."
Write-Host "Next:"
Write-Host "  git remote add origin git@github.com:mildxw/pit.git"
Write-Host "  git push -u origin main"
Write-Host "  git push -u origin pr4"
