import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemePickerMobileComponent } from './theme-picker-mobile.component';

describe('ThemePickerMobileComponent', () => {
  let component: ThemePickerMobileComponent;
  let fixture: ComponentFixture<ThemePickerMobileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ThemePickerMobileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThemePickerMobileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
