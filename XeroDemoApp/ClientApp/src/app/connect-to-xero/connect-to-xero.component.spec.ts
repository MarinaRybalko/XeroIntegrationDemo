import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConnectToXeroComponent } from './connect-to-xero.component';

describe('ConnectToXeroComponent', () => {
  let component: ConnectToXeroComponent;
  let fixture: ComponentFixture<ConnectToXeroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConnectToXeroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConnectToXeroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
