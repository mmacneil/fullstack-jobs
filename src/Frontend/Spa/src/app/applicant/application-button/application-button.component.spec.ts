import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationButtonComponent } from './application-button.component';

describe('ApplicationButtonComponent', () => {
  let component: ApplicationButtonComponent;
  let fixture: ComponentFixture<ApplicationButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
