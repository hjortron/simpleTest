# simpleTest
Тестовое задание от SC

We would like to build domain name based blacklist/filter. Our blacklist database contains around 20000 domain names, including Top Level Domains, Second-level and Lower level domains, for example:
.biz
stackoverflow.com
ru.wikipedia.com
We would like to perform lookups to see if input URL domain name is listed in the blacklist. Simply looping over the list and doing string based comparison is not an option. If I do 1000 lookups it's simply too slow.
Lookup must return positive match if TLD is blacklisted, for example, if .biz TLD is blacklisted, mycompany.biz should return positive match.
On the other hand, fr.wikipedia.com should not match, because sub-domain is different.
Your task is to write sample console application with С# that selects 1000 random domains from the list attached and perform lookups of those random domain names against the full blacklist. 
