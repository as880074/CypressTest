it('can add new todo items', () => {
  const newItem = 'admin@gmail.com';
  const password = '123';
  // 1. 開啟 ToDo 應用程式
  cy.visit('https://localhost:7268/');

  // 2. 在建立項目的輸入框中輸入項目名稱並送出
  cy.get('[name=Email]').type(`${newItem}`);
  cy.get('[name=Password]').type(`${password}{enter}`);
});