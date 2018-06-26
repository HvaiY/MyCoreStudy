import { MyFirstABPProjectTemplatePage } from './app.po';

describe('MyFirstABPProject App', function() {
  let page: MyFirstABPProjectTemplatePage;

  beforeEach(() => {
    page = new MyFirstABPProjectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
