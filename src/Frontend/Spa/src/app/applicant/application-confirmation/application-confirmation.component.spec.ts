import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationConfirmationComponent } from './application-confirmation.component';

describe('ApplicationConfirmationComponent', () => {
  let component: ApplicationConfirmationComponent;
  let fixture: ComponentFixture<ApplicationConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationConfirmationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
