@echo off
setlocal

echo 🧪 Comprovant llibreries necessàries...

IF NOT EXIST "gson-2.10.1.jar" (
    echo ❌ FALTA: gson-2.10.1.jar
    goto finalError
)
IF NOT EXIST "mysql-connector-j-9.3.0.jar" (
    echo ❌ FALTA: mysql-connector-j-9.3.0.jar
    goto finalError
)
IF NOT EXIST "bson-4.11.1.jar" (
    echo ❌ FALTA: bson-4.11.1.jar
    goto finalError
)

echo ✅ Totes les llibreries són presents.
echo.
echo 🔧 Compilant fitxers Java...
javac -cp ".;gson-2.10.1.jar;mysql-connector-j-9.3.0.jar;bson-4.11.1.jar" *.java

IF %ERRORLEVEL% NEQ 0 (
    echo ❌ ERROR: Hi ha errors de compilació.
    goto finalError
)

echo ✅ Compilació correcta.
echo.
echo ▶️ Executant Main.java...
java -cp ".;gson-2.10.1.jar;mysql-connector-j-9.3.0.jar;bson-4.11.1.jar" Main

IF %ERRORLEVEL% NEQ 0 (
    echo ❌ ERROR: Error en l'execució de Main.
    goto finalError
)

echo.
echo ✅ Execució finalitzada correctament.
goto fi

:finalError
echo.
echo 🔴 Revisar els missatges anteriors. El procés s'ha aturat.
pause
exit /b

:fi
pause
endlocal
