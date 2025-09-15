# ERT

#Publish (framework-dependent)
dotnet publish -c Release -r win-x64 --self-contained=false -p:PublishSingleFile=false

#Servisi cmd ile oluştur/başlat
sc stop ERTMqttHost
sc delete ERTMqttHost

sc create ERTMqttHost binPath= "C:\mqtt\ERT.MqttHost.exe" DisplayName= "ERT MQTT Host" start= auto
sc description ERTMqttHost "ERT MQTT Broker Worker Service"

sc start ERTMqttHost
sc query ERTMqttHost

