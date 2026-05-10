import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogRequestMail } from './dialog-request-mail';

describe('DialogRequestMail', () => {
  let component: DialogRequestMail;
  let fixture: ComponentFixture<DialogRequestMail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DialogRequestMail],
    }).compileComponents();

    fixture = TestBed.createComponent(DialogRequestMail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
