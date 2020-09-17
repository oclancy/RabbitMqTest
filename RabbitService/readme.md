
setup local nuget
1) nuget sources ls
2) nuget sources add "<local nuget folder>"
3) nuget sources ls


build nuget package
nuget <path to project>

clear local nuget
rm -r <local nuget folder>
add to local nuget
nuget add .\RabbitService.1.0.0.nupkg -source H:\nuget


//target project
set up nuget source(if not done)
select local nuget source
uninstall existing version (if present)
delete existing package (unless version changed)
rm -r C:\Users\olive\.nuget\packages\rabbitservice

