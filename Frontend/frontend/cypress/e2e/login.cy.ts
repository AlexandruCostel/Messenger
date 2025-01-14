describe('Test Login', () => {
  it('should login successfully', () => {
    cy.visit('http://localhost:4200/login');
    cy.get('input[name="username"]').type('test');
    cy.get('input[name="password"]').type('test');
    cy.get('button[type="submit"]').click();
    cy.url().should('include', '/main-page');
    cy.get('button.create-post-btn').should('be.visible');

    const navButtons = [
      { selector: 'button.nav-btn', text: 'Main Page' },
      { selector: 'button.nav-btn', text: 'Friends' },
      { selector: 'button.nav-btn', text: 'Add Friends' },
      { selector: 'button.nav-btn', text: 'Job Announcements' },
      { selector: 'button.nav-btn', text: 'Profile' },
    ];

    navButtons.forEach((button) => {
      cy.get(button.selector).contains(button.text).should('be.visible');
    });
  });
});
