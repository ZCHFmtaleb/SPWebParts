Echo Off
SET SPLANGEXT=cs

Echo Backing up previous version of generated code ... 
IF NOT EXIST .\PreviousVersionGeneratedCode MkDir .\PreviousVersionGeneratedCode
IF EXIST ZF.%SPLANGEXT% xcopy /Y/V ZF.%SPLANGEXT% .\PreviousVersionGeneratedCode

Echo Generating code ...
SPMetal  /web:http://zf-spdev:1111/  /code:ZF.%SPLANGEXT% /parameters:SPMetal.xml