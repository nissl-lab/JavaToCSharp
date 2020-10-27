# Java2CSharp
a lightweight tool can help convert Java code to C# code. The tool is a batch of replacement rules and a configuration file to set up the rules. It used to be located in `npoi repo`/tools/JavaToCSharp

# Story of Java2CSharp tool
At the very beginning of NPOI development, it's very hard and boring job to convert Java code to C# in the first and second year. Then I started writing this tool to help convert Java to C# as quick as possible. Anyway, this tool did help a lot in the development of NPOI and let me focus on fixing bugs instead of repeat the replacement action again and again. 

# How to setup this tool
Open app.config and add new rules into rules node under the J2CS section 
```
<J2CS>
    <Rules>
      <add name="Array.Copy" pattern="System\.arraycopy" replacement="Array.Copy"/>
      <add name="toByteArray" pattern="toByteArray" replacement="ToArray"/>
      <add name="Character.isLetter" pattern="Character.isLetter" replacement="Character\.isLetter"/>
      ...
    </Rules>
</J2CS>
```

# PowerShell scripts
To convert a single file, run `.\JavaToCSharp <filename>` 


To convert a batch of files under the folder, call [scripts/run.ps1](https://github.com/nissl-lab/JavaToCSharp/blob/main/scripts/run.ps1)

