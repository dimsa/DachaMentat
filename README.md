# DachaMentat
OrangePi-Based Solution for the automation of private farming

## Steps to Deploy
#0) For this you need Orange Pi 4 LTS with at least 16 gb of MMC.
#0.1) Orange PI 4 LTS is very demanding to Power Supply. The best UX I goo using Xiaomi Powerbank with Quick Charge.

#1) Make MicroSD Card with last Orange Pi Ubuntu Desktop or Armbian Desktop. It works both, but I used original Armbian OS

#2) Insert to Orange PI, setup languages and etc. I use for login root and password orangepi. You can create your account

#2*) You can also install all what you want. When you will be on the paragraph 4, all your work will be flashed on emmc

#3) Now it's better to use SSH. Install Putty for Windows. Find the IP of OrangePI (I used Router Page for this) and make connection to it
.
#4) Flash emmc. Go to Linux Terminal and then do the following command (Here is video https://www.youtube.com/watch?v=t8IkQBq34WA)
sudo /usr/sbin/nand-sata-install
#Select Boot from Emmc - sytem on emmc
# Then - yes
# Then select ext4 filesystem
# Switch device off.

#6) Switch device On and use Putty to connect through SSH

#7) Update repository. Run in terminal following commands. Press Y on every question
sudo apt-get update
sudo apt-get upgrade

#8) Install aspnet-core-runtime
sudo apt-get install aspnetcore-runtime-7.0 
sudo apt-get install dotnet-sdk-7.0

#9)
cd /home
git clone https://github.com/dimsa/DachaMentat

cd /home/DachaMentat/src/DachaMentat
dotnet build "DachaMentat.csproj" -c Release -o /app/build
dotnet publish "DachaMentat.csproj" -c Release -o /app/publish /p:UseAppHost=false
