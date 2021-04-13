<a name="markedpp-execution-action"></a>

# Markedpp execution action

This action prints "Hello World" or "Hello" + the name of a person to greet to the log.

<a name="inputs"></a>

## Inputs

<a name="manifest"></a>

### `manifest`

**Required** Path to the manifest file. [format](#manifest-file)

<a name="outputs"></a>

## Outputs

<a name="output"></a>

### `output`

Folder where generated documents are produced.

<a name="example-usage"></a>

## Example usage

```yaml
uses: gatewayprogramminschool/action-markedpp
with:
  manifest: '/.gps.markedpp'
  GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
```

<a name="manifest-file"></a>

## Manifest File

The format of .gps.markedpp

```json
{
    "rootSourceFiles": [
        "./example/manual.md"
    ],
    "outputDirectory": "./example/transformed",
    "options": {
        "gfm": true,
        "include": true,
        "toc": true,
        "numberedheadings": true,
        "ref": true,
        "breaks": true,
        "tags": true,
        "level": 3,
        "minlevel": 1,
        "autonumber": true,
        "autoid": true,
        "githubidv": false    
    }
}
```
- **rootSourceFiles** is an array of filenames (including path if necessary) that points to files that are to be the beginning of a transformation.  You can have multiple transformations across multiple root files.
- **outputDirectory** specifies the location for the transformed files to be written to.
- **options** specifies the [markedpp options](https://github.com/commenthol/markedpp#usage) to pass to the pre-processor.