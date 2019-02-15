param (
	[alias('b')]
	[switch]
	$Build,
	[alias('r')]
	[switch]
	$Run
)

if ($Run) {
	dotnet publish -c Release --self-contained true -r win10-x86
} elseif ($Build) {
	dotnet build
} else {
	dotnet build
}