# Onova.ReleaseNotes
[![Version](https://img.shields.io/nuget/v/Onova.ReleaseNotes)](https://nuget.org/packages/Onova.ReleaseNotes)  

[Onova](https://github.com/Tyrrrz/Onova) extension that provides support for release notes.

## Documentation
This package provides an extension to the UpdateManager.
The extension will work only if you use WebPackageResolver.
After publishing your application with the manifest, create a text file that has the same name as the version package, except change the file extension to .rn.
Example:
```
    MANIFEST
    DummyApp-1.2.3.zip
    DummyApp-1.2.3.rn
    websetup.exe
```
Write your desired changes into the release notes file, save and close.
In your application, use the following code:
```
using (var mgr = new UpdateManager(new WebPackageResolver("https://dummy.com/files/MANIFEST"), new ZipPackageExtractor()))
{
  var result = await mgr.CheckForUpdatesAsync();
  var releaseNotes = await mgr.GetReleaseNotes(result.LastVersion);
  ...
}
```
You can download any version that is available in the manifest.

If you are using my other package, [Onova.Publisher](https://github.com/dady8889/Onova.Publisher), it will make sure to create an empty .rn file for you.

## Contributions
I am open to suggestions, PRs, bug reports.
Any contribution is welcome.

## Licensing
My provided code is licensed under the MIT license.
