import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreUserInfoPageComponent } from './ire-user-info-page.component';

describe('IreUserInfoPageComponent', () => {
  let component: IreUserInfoPageComponent;
  let fixture: ComponentFixture<IreUserInfoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreUserInfoPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreUserInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
