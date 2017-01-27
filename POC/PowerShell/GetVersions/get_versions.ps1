# Creates an element with $name that has a value attribute with $value
function Create_Element($document, $name, $value)
{
	$element = $document.CreateElement($name)
	$element.SetAttribute("value", $value);
	$element
}

function Process_Path($document, $currentElement, $current_path)
{
	# Change to the current path
	pushd $current_path
	
	# Create a directory element for this path
	$new_element = Create_Element $document "Directory" $current_path
	[Void]$currentElement.AppendChild($new_element)

	# Evaluate each child in this path			
	foreach ($child in Get-ChildItem)	
	{
		if (Test-Path $child.FullName -PathType Container)
		{
			# This is a directory so process its children
			Process_Path $document $new_element $child
		}
		elseif ([IO.Path]::GetExtension($child) -eq ".dll" -or [IO.Path]::GetExtension($child) -eq ".exe")
		{
			# This is an assembly. Add an element with an attribute containing the version
			$assembly_element = Create_Element $document "File" $child
			[Void]$new_element.AppendChild($assembly_element)
			
			# Get the version
			$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($child.FullName)
			
			if ($version -ne $null)
			{
				# Add version attribute
				$assembly_element.SetAttribute("version", $version.FileVersion.ToString())
			}
		}
#		else
#		{
#			$foo = Create_Element $document "File" $child
#			$new_element.AppendChild($foo)
#		}
	}
	popd
}

function List_Directories()
{
	param
	(
		$currentElement,
		[string]$current_dir
	)
	
	pushd $current_dir
	
	$children = dir
	
	$current_dir
	
	foreach ($child in $children)
	{
		if (Test-Path $child.fullname -pathtype container)
		{
			List_Directories $child.fullname
		}
		else
		{
			#[IO.PATH]::GetFileNameWithoutExtension($child.fullname)
			$child.fullname
		}
	}
	
	popd
}

#Add-Type -AssemblyName System.Xml

if ($args.Length -gt 1)
{
	$path = $args[0]
}
else
{
	
	$path = $PWD
}

$xmlDoc = New-Object System.Xml.XmlDocument
$root = $xmlDoc.CreateElement("FileSystem")

# Appending [void] prevents the return value from being displayed
[Void]$xmlDoc.AppendChild($root)
 
Process_Path $xmlDoc $root $path

$outputFile = Join-Path $path "output.xml" 

$outputFile

$xmlDoc.Save($outputFile)
Get-Content($outputFile)

<#
This is a block comment.
#>

