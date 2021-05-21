# Kontaktsplitter
Projektaufge Kontaktsplitter im Rahmen der Vorlseung "Softwarequalität".

## User Stories
- Als Sachbearbeiter wünsche ich mir die Funktion, welche eine Anrede mit Namen in ihre Bestandteile zerlegt und darstellt, weil ich diese Bestandteile in einer Datenbank abspeichern möchte. 
Ich weiß ich bin fertig, wenn eine Anrede in ihren Bestandteilen dargestellt wird.  Zum Beispiel, dass Frau Dr. Sandra Berger in “Frau”, “Dr.”, “Sandra” und “Berger” aufgeteilt wird.
- Als Sachbearbeiter wünsche ich mir die Funktion, eine automatisch generierte, förmliche Briefanrede zu erstellen, sodass ich diese Vorlage später verwenden kann.
Ich weiß ich bin fertig, wenn eine förmliche Anrede zu der getätigten Eingabe erstellt wird. Zum Beispiel, dass bei Frau Sandra Berger die Briefanrede “Sehr geehrte Frau Sandra Berger” entsteht.
- Als Sachbearbeiter wünsche ich mir die Möglichkeit eigene Titel zu definieren, weil ich somit neue Titel miteinbeziehen kann.
Ich weiß ich bin fertig, wenn der hinzugefügte Titel als solcher von dem Kontaktsplitter erkannt und richtig zugeteilt wird. 
- Als Sachbearbeiter wünsche ich mir, dass die vom Kontaktsplitter zurückgegebenen Bestandteile bearbeitet werden können, damit persönliche Änderungen im Nachhinein getätigt werden können.
Ich weiß ich bin fertig, wenn ich die ausgegebenen Bestandteile noch im Nachhinein bearbeiten kann.
- Als Sachbearbeiter wünsche ich mir, falls es von der Eingabe ableitbar ist, die Rückgabe des Geschlechts der eingegebenen Person, damit ich dieses jeweils zuordnen kann. 
Ich weiß ich bin fertig, wenn das Geschlecht, bei passenden Eingaben, zurückgegeben wird. Zum Beispiel, dass bei der Eingabe Frau Sandra Berger als Geschlecht “Female” zurückgegeben wird.
- Als Sachbearbeiter wünsche ich mir die Möglichkeit eigene Anreden mit einer Sprache, dem zugehörigen Geschlecht und einer Briefanrede zu definieren, weil ich somit neue Anreden miteinbeziehen kann.
Ich weiß ich bin fertig, wenn die Anrede als solcher von dem Kontaktsplitter erkannt und richtig zugeteilt wird und auch die Sprache, Geschlecht und Briefanrede passend zurückgegeben werden.

## Ausprobieren
Die Benutzeroberfläche des MVPs des Kontaktsplitters wird auf einem S3 Server auf der AWS Cloud betrieben. Um auf diesen zugreifen zu können, wird folgende URI verwendet:
[Webseite](http://contactsplitter.s3-website.eu-central-1.amazonaws.com/)
