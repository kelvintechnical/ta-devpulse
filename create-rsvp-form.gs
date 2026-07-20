/**
 * DevPulse RSVP — one-click Google Form creator
 *
 * How to run:
 * 1. Go to https://script.google.com → New project
 * 2. Delete any default code, paste this entire file
 * 3. Save → select createDevPulseRsvpForm → Run
 * 4. Approve Google permissions when prompted
 * 5. Check View → Logs (or Executions) for the form URL + edit URL
 *
 * The form lands in your Google Drive. Responses auto-link to a Sheet.
 */

function createDevPulseRsvpForm() {
  var form = FormApp.create('DevPulse RSVP — Tech Affiliates Build Session');

  form.setDescription(
    'Build a live web dashboard in one morning — APIs, JavaScript, and deploy to the internet. No experience required.\n\n' +
    '📍 Giddy-Up Coffee House, Greenville NC\n' +
    '📅 Saturday, September 12 (time TBD — we\'ll confirm)\n' +
    '💻 Bring a laptop if you have one\n\n' +
    'RSVP so we know how many seats / tables to set up. Questions? Reach Kelvin (@kelvinintech).'
  );

  form.setCollectEmail(true);
  form.setAllowResponseEdits(true);
  form.setLimitOneResponsePerUser(false);
  form.setConfirmationMessage(
    'You\'re on the list — see you at Giddy-Up. We\'ll share final time + details closer to Sept 12.'
  );

  form.addTextItem()
    .setTitle('Full name')
    .setRequired(true);

  form.addMultipleChoiceItem()
    .setTitle('Are you coming?')
    .setChoiceValues(['Yes', 'Maybe', 'Can\'t make it'])
    .setRequired(true);

  form.addMultipleChoiceItem()
    .setTitle('Coding comfort level?')
    .setChoiceValues(['Brand new', 'Some basics', 'Comfortable'])
    .setRequired(true);

  form.addMultipleChoiceItem()
    .setTitle('Do you have a laptop you can bring?')
    .setChoiceValues(['Yes', 'No', 'Sharing with someone'])
    .setRequired(true);

  form.addMultipleChoiceItem()
    .setTitle('How did you hear about this?')
    .setChoiceValues(['Instagram', 'Discord', 'Friend', 'Other'])
    .setRequired(false);

  form.addTextItem()
    .setTitle('GitHub username (optional)')
    .setRequired(false);

  form.addParagraphTextItem()
    .setTitle('Anything we should know? (dietary, accessibility, questions)')
    .setRequired(false);

  // Linked Sheet for easy Yes/Maybe counts
  var sheet = SpreadsheetApp.create('DevPulse RSVP Responses');
  form.setDestination(FormApp.DestinationType.SPREADSHEET, sheet.getId());

  var publishedUrl = form.getPublishedUrl();
  var editUrl = form.getEditUrl();
  var sheetUrl = sheet.getUrl();

  Logger.log('✅ Form created');
  Logger.log('Share this link: ' + publishedUrl);
  Logger.log('Edit form:      ' + editUrl);
  Logger.log('Responses sheet: ' + sheetUrl);

  // Optional: email yourself the links
  var email = Session.getActiveUser().getEmail();
  if (email) {
    MailApp.sendEmail({
      to: email,
      subject: 'DevPulse RSVP form is ready',
      body:
        'Share this RSVP link:\n' + publishedUrl + '\n\n' +
        'Edit the form:\n' + editUrl + '\n\n' +
        'Responses spreadsheet:\n' + sheetUrl
    });
  }

  return {
    publishedUrl: publishedUrl,
    editUrl: editUrl,
    sheetUrl: sheetUrl
  };
}
