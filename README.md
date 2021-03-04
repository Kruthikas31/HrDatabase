# HR Portal

 About HR Portal

## Getting Started

These instructions will get a copy of the project up and running on local machine for development and testing purposes. 

## Prerequisites

* Install Git Gui [Click Here](http://software.vitalaxis.com/Software/Tools/Git_tools/) 
* Generate ssh-gen or take help of MIS team. Send email to [helpdesk@starmark.mail](mailto:helpdesk@starmark.mail)
* Login to  http://vagit.vitalaxis.com with Starmark windows credentials.
* Open the JARVIS.sln in Visual Studio 2019


Check the associated email 

```console
c:\MyProject\MainFolder> git config user.email 
```

if email change is necessary

``` 
c:\MyProject\MainFolder>git config --global user.email "myemail@starmark.mail" 
```

## How to get the project code

 Clone creates a copy of project in local machine for the remote repository.

```
c:\MyProject\MainFolder> git clone http://vagit.vitalaxis.com/Starmark-HRPortal/JARVIS.git

```
 To Clone a specfic branch , use the below command. To checkout other 
 than **master**, replace **master** with branch name.  
 
```
c:\MyProject\MainFolder> git clone -b master http://vagit.vitalaxis.com/Starmark-HRPortal/JARVIS.git

```
Once the repo is downloaded to the local machine, navigate to the project folder. One hidden folder .git persists the actual local repository. 

```
 c:\MyProject\MainFolder>cd HRPortal
```

First complete the required changes. Find out the files changed in the folder.     

```
c:\MyProject\MainFolder\HRPortal>git status  'List the file(s) changed 
c:\MyProject\MainFolder\HRPortal>git add .   'Add the changes  to staging which includes new files. 
```

Once the staging is complete, time to commit the changes in the local repository.

Note: Local machine acts as a local repostiory.

```
c:\MyProject\MainFolder\HRPortal> git commit -am "Comments must be more in detail like What you did &  why you did it"
```

Time to push the code to remote repository. Before pusing the code, check the target branch is correct using git status.

Perfect, multiple developers push the changes to the remote repoitory, but the local copy will be out of sync with master/release/sprint branch. Pull command will get latest code from the remote repository branch. Explicltiy specifcying the branch name will pull from the speficed branch.

```
c:\MyProject\MainFolder\HRPortal>git pull
'''
or
'''
c:\MyProject\MainFolder\HRPortal>git pull origin branchName
```


What will happen if the last commit was  accident?
    
   Check git logs to see the recent commits. Use git reset --hard <commit sha>, it will completely discarded the changes from the last commit. 
  The files/folder reverted back to the point speficed. It’s like the changes never happened.

