So you want to edit the Super Snail wiki? Well it would be a pleasure to have you here! We have tried to make a short guide below to help make your first contribution as easy as possible!
1. First Steps
Before you can go anywhere with the wiki, you're going to have to log in to an account. This can either be done through the "log in" button in the top right of screen or you can use **this link**. We recommend making your Wiki username the same as your discord or in game name.

Once you're logged in to an account, that's it! You're free to start editing! You won't be able to edit any important/frontline pages but otherwise, the world is your oyster! I will go through some general best practices as well as some tips and tricks in the following sections.

We also recommend joining the Super Snail Discord to keep up to date with new information as it is posted. It is also where we normally reach out to frequent editors

1. Editing Pages
Before we get into the meat of changing the wiki, you've actually got to find the edit button first. This is normally in the top right of your screen (next to the search bar) but it may also be in a drop down arrow. If you instead see "View Source" it means that the page your are trying to edit it protected. Most protected pages become available after editing consistently for a while. Feel free to look around at the body of the page even if you can't edit it. 

1. Relics, Shells, and Cargo
You may notice that the relic and shell pages have quite a different format to our normal wiki pages. This is because they are stored in a "Cargo Table" and have to be treated a little differently. You can check everything that's currently stored in a Cargo Table on the Cargo Tables page but you really only have to worry about Relics, Shells, and Resonances. I **strongly** recommend using the documentation on Template:CargoRelic and Template:Shell alongside an existing page that uses the template to understand exactly what you need to do when editing a relic. Its fairly self explanatory once you've had a look at a few.

1. Everything Else
Honestly the above makes up ~1800 of our 2100 pages so there's not a whole let else that is regularly edited. This is mostly dedicated to realms, domains, and other misc features. If you can't find a feature in the wiki (e.g. Hatchery or the Talent Market), feel free to make a page for it and let people know!

In general, our priority is the to have the content *in* the wiki. Someone can always come back later to make it look more presentable. If you're not sure, include the info, put a message in the wiki channel on discord, and someone will have a look at it when they get the chance. When it comes to appearances, try to implement the templates and layouts used in other pages to keep everything consistent. I'll run through a few of our notable templates below:

1. Templates
These are basically resources created by editors to help make the wiki more consistent. You use "invoke" a template with double curly brackets or as follows <nowiki> </nowiki> where each term after the pipe, "|", is a new argument to the template. You can read the documentation page for a template by searching for "Template:TemplateName". These will normally break down all of the arguments and how to use the template properly.

</nowiki>''' - Template:List which is actually what is being used for this list. It just simplifies standard HTML list formatting and lets you do up to 20 elements, ordered (1., 2., 3.) or unordered (dot points). This is only a new template so you won't see it much but please try to use it wherever possible.
|**<nowiki></nowiki>** - Template:CargoRelic this is the template used for all relics currently in the wiki (excluding Supreme relics) and a template you should be very familiar with
|**<nowiki>  </nowiki>** - Template:relink creates a colored link to a page, Template:reimg creates an image that links to a page, and Template:retext creates colored text with no link. These all come from working on relics, hence the "re" at the start.
|**<nowiki>  </nowiki>** - Template:RewardIcon, Template:RewardIconWithLink, Template:RewardIconWithLink is a standard icon size for use in tables/text with variations for the inclusion of text or links as needed. 
| **<nowiki>Category:Notice templates</nowiki>** - Not a template this time but a collection of them. Use these to tag pages as needed. Most notably when they are a WIP, relate to unreleased content, or contain heavy spoilers. Normally easiest to search for "Category:Notice Templates" to find them but here is a link **Category:Notice Templates**.
| **<nowiki></nowiki>** - A bit of a more niche one. It allows you to clear any existing formatting and basically start anything below on the next line. Its useful when images are messing with your formatting.
}}

1. Uploading and Using Files
The File Upload page is your one stop shop for adding images and other files to the wiki. Its a pretty simple process when uploading, just try to pick a name that reflects the image as best as possible, e.g. using a relic's or enemy's name, and keep all images in the .png format wherever possible. Give it a quick description and you're all set to upload! From here, you actually have to use the file. The cargo relic template has been made to automatically include the image using a relic's name but you will have to include everything else yourself.

Your baseline for including an image on a page is **<nowiki></nowiki>**. This is the absolute bare minimum for including an image and should typically be avoided. Most images should be wrapped/included using one of the relevant templates above, but if you're absolutely sure it should be included directly into the  age, try and find the option below that looks the best on the page.

-*Frameless** - This is basically just including the image directly into the page. <nowiki></nowiki> will produce:

-*Thumb** - This is probably what you've seen on wikipedia. <nowiki></nowiki> will produce:

Just a few more notes about the parameters when uploading an image directly:

1. Tables
Love them or hate them, everything boils down to tables of data. They're normally best learnt through experience but I will go through some important formatting here.

-*Extended Version**
 <nowiki>
 

[WIKITABLE OMITTED]

</nowiki>

-*Compact Version**
 <nowiki>
 

[WIKITABLE OMITTED]

</nowiki>

 

[WIKITABLE OMITTED]

1. Final Notes
Just practice! You can copy a page's content (either from the edit or view source tab) to a new page and edit it to your pleasure without impacting anything. We normally recommend putting it on your user page which you can find by searching "User:x" and replacing the x with your username. Try finding my user page first, my username is "Daedulus1st" (If you're seeing this after they finally update it to LavaLamp, please ping me on discord!).

Only thing to note, please **DO NOT** use one of the cargo templates on your user page. It will be included into the Cargo table automatically and we will have to manually refresh the data to remove it. Just to reiterate, this means do not use the CargoRelic, CargoRelicResonance, Mirage, Shell, or missing info templates on your page.

Category:Editor
Category:Guides